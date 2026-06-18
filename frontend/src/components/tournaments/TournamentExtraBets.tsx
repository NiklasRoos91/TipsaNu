import React, { useState, useEffect, useMemo } from 'react';
import { Search } from 'lucide-react';
import { ActionButton } from '../commons/ActionButton';
import { ExtraBetOptionForm } from '../extraBets/ExtraBetOptionForm';
import { ExtraBetCard } from '../extraBets/ExtraBetCard';
import type { ExtraBetOptionForUser } from '../../types/extrabetTypes';
import { useGetExtraBetOptions } from "../../hooks/extraBets/useGetExtraBetOptions";
import { CategoryFilterBar } from '../commons/CategoryFilterBar';
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
  const [searchQuery, setSearchQuery] = useState('');
  const [sortOrder, setSortOrder] = useState<'soonest' | 'latest'>('soonest');

  const categories: ExtraBetFilterEnum[] = [
    ExtraBetFilterEnum.All,
    ExtraBetFilterEnum.Open,
    ExtraBetFilterEnum.Closed,
    ...(isAdmin ? [ExtraBetFilterEnum.NeedsCorrection] : []),
  ];

  const backendStatus = selectedFilter === ExtraBetFilterEnum.NeedsCorrection
    ? ExtraBetFilterEnum.Closed
    : selectedFilter;

  const { options: extraBets, loading, error, refetch: refetchOptions } =
    useGetExtraBetOptions(Number(tournamentId), backendStatus);

  const { options: closedBets } = useGetExtraBetOptions(
    isAdmin ? Number(tournamentId) : 0,
    ExtraBetFilterEnum.Closed
  );

  const displayedBets = useMemo(() => {
    const filtered = extraBets.filter(bet =>
      bet.name.toLowerCase().includes(searchQuery.toLowerCase())
    );
    return [...filtered].sort((a, b) => {
      if (!a.expiresAt && !b.expiresAt) return 0;
      if (!a.expiresAt) return 1;
      if (!b.expiresAt) return -1;
      const diff = new Date(a.expiresAt).getTime() - new Date(b.expiresAt).getTime();
      return sortOrder === 'soonest' ? diff : -diff;
    });
  }, [extraBets, searchQuery, sortOrder]);

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
            badges={{ [ExtraBetFilterEnum.NeedsCorrection]: closedBets.length }}
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

      {/* Search and sort bar */}
      <div className="flex flex-col sm:flex-row gap-3">
        <div className="relative flex-1">
          <Search size={15} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
          <input
            type="text"
            value={searchQuery}
            onChange={e => setSearchQuery(e.target.value)}
            placeholder="Sök extratips..."
            className="w-full pl-9 pr-4 py-2 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none transition-all text-sm font-medium text-slate-900 shadow-inner"
          />
        </div>
        <select
          value={sortOrder}
          onChange={e => setSortOrder(e.target.value as 'soonest' | 'latest')}
          className="bg-slate-50 border border-slate-200 rounded-xl px-3 py-2 text-sm font-medium text-slate-700 outline-none focus:ring-4 focus:ring-accent/10 focus:border-accent transition-all"
        >
          <option value="soonest">Sortera: Kortast tid kvar</option>
          <option value="latest">Sortera: Längst tid kvar</option>
        </select>
      </div>

      {/* List with extrabet options */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-4">
        {loading && <div className="col-span-full">Laddar extratips...</div>}
        {error && <div className="col-span-full text-red-500">{error}</div>}

        {!loading && !error && extraBets.length === 0 && (
          <div className="col-span-full text-center p-12 bg-slate-50 rounded-xl border-2 border-dashed border-slate-200 text-slate-400">
            {selectedFilter === ExtraBetFilterEnum.NeedsCorrection
              ? 'Inga stängda extratips att rätta just nu.'
              : 'Inga extratips skapade för denna turnering ännu.'}
          </div>
        )}

        {!loading && !error && extraBets.length > 0 && displayedBets.length === 0 && (
          <div className="col-span-full text-center p-12 bg-slate-50 rounded-xl border-2 border-dashed border-slate-200 text-slate-400">
            Inga extratips matchar "{searchQuery}".
          </div>
        )}

        {!loading && !error && displayedBets.map((bet: ExtraBetOptionForUser) => {
          const initialPrediction: { betId: string; selectedOption: string } | undefined = bet.myBet
            ? { betId: bet.myBet.extraBetId.toString(), selectedOption: bet.myBet.value ?? '' }
            : undefined;

          return (
            <ExtraBetCard
              key={bet.optionId}
              bet={bet}
              isAdmin={isAdmin}
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