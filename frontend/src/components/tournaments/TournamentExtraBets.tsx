import React, {useState, useEffect} from 'react';
import { ActionButton } from '../commons/ActionButton';
import { ExtraBetOptionForm } from '../extraBets/ExtraBetOptionForm';
import { ExtraBetCard } from '../extraBets/ExtraBetCard';
import type { ExtraBetOptionForUser } from '../../types/extrabetTypes';
import { useGetExtraBetOptions } from "../../hooks/extraBets/useGetExtraBetOptions";
import { CategoryFilterBar   } from '../commons/CategoryFilterBar';
import { ExtraBetFilterEnum } from '../../types/enums/extraBetEnums';

interface TournamentExtraBetsProps {
  tournamentId: string;
  isAdmin: boolean;
}

export const TournamentExtraBets: React.FC<TournamentExtraBetsProps> = ({
  tournamentId,
  isAdmin
}) => {
  const [showForm, setShowForm] = React.useState(false);
  const [selectedFilter, setSelectedFilter] = useState<ExtraBetFilterEnum>(ExtraBetFilterEnum.All);
  const categories: ExtraBetFilterEnum[] = [
    ExtraBetFilterEnum.All,
    ExtraBetFilterEnum.Open,
    ExtraBetFilterEnum.Closed,
  ];

  const { options: extraBets, loading, error, refetch: refetchOptions } =
    useGetExtraBetOptions(Number(tournamentId), selectedFilter);

  const handleFormCreated = () => {
    setShowForm(false);
    refetchOptions();
  };  

  useEffect(() => {
    if (selectedFilter) refetchOptions();
  }, [selectedFilter, refetchOptions]);

return (
    <div className="space-y-6">
      {/* Header */}
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
        <h2 className="text-2xl font-bold text-primary">Extratips</h2>

        <div className="flex items-center gap-2 sm:w-auto">
          {/* Filter/Category bar */}
          <CategoryFilterBar
            categories={categories}
            currentCategory={selectedFilter}
            onCategoryChange={setSelectedFilter}
          />

          {isAdmin && (
            <div className="w-auto ml-auto">
              <ActionButton
                label="Skapa ny"
                onClick={() => setShowForm(!showForm)}
                isActive={showForm}
              />
            </div>
          )}
        </div>
      </div>

      {/* Form */}
      {isAdmin && showForm && (
        <ExtraBetOptionForm
          tournamentId={tournamentId}
          onCreated={handleFormCreated}
          onCancel={() => setShowForm(false)}
        />
      )}

      {/* List with extrabet options */}
      <div className="grid gap-4">
        {loading && <div>Laddar extratips...</div>}
        {error && <div className="text-red-500">{error}</div>}
        {!loading && !error && extraBets.length === 0 && (
          <div className="text-center p-12 bg-slate-50 rounded-xl border-2 border-dashed border-slate-200 text-slate-400">
            Inga extratips skapade för denna turnering ännu.
          </div>
        )}

        {!loading && !error && extraBets.map((bet: ExtraBetOptionForUser) => {
          const initialPrediction: { betId: string; selectedOption: string } | undefined = bet.myBet
            ? { betId: bet.myBet.extraBetId.toString(), selectedOption: bet.myBet.value }
            : undefined;

          return (
            <ExtraBetCard 
              key={bet.optionId} 
              bet={bet} 
              initialPrediction={initialPrediction}
              isExpired={bet.expiresAt ? new Date(bet.expiresAt) < new Date() : false} 
              onSavePrediction={(prediction) => console.log('Saved prediction:', prediction)} 
            />
          );
        })}
      </div>
    </div>
  );
};