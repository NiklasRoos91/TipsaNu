
import React, {useState} from 'react';
import { Shield } from 'lucide-react';
import { ActionButton } from '../commons/ActionButton';
import { ExtraBetOptionForm } from './ExtraBetOptionForm';
import { ExtraBetCard } from './ExtraBetCard';

interface TournamentExtraBetsProps {
  tournamentId: string;
  isAdmin: boolean;
}

export const TournamentExtraBets: React.FC<TournamentExtraBetsProps> = ({
  tournamentId,
  isAdmin
}) => {
  const [showForm, setShowForm] = React.useState(false);

  const [extraBets, setExtraBets] = useState([
    { id: 1, name: 'Vem gör flest mål?', description: 'Skytteligan 2026', points: 10, expiresAt: '2026-02-20T12:00' },
    { id: 2, name: 'Vem vinner turneringen?', description: 'Slutspel 2026', points: 20, expiresAt: '2026-02-25T18:00' },
  ]);

  // Callback när ett nytt extrabet skapas
  const handleFormCreated = () => {
    setShowForm(false);

    // För mock: lägg till ett nytt extrabet i listan
    setExtraBets(prev => [
      ...prev,
      {
        id: prev.length + 1,
        name: 'Ny fråga från formulär',
        description: 'Beskrivning...',
        points: 10,
        expiresAt: '2026-03-01T12:00'
      }
    ]);
  };

return (
    <div className="space-y-6">
      {/* Header */}
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
        <h2 className="text-2xl font-bold text-primary">Extratips</h2>
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

      {/* Form */}
      {isAdmin && showForm && (
        <ExtraBetOptionForm
          tournamentId={tournamentId}
          onCreated={handleFormCreated}
          onCancel={() => setShowForm(false)}
        />
      )}

      {/* Lista med extrabet-options */}
      <div className="grid gap-4">
        {extraBets.length === 0 ? (
          <div className="text-center p-12 bg-slate-50 rounded-xl border-2 border-dashed border-slate-200 text-slate-400">
            Inga extratips skapade för denna turnering ännu.
          </div>
        ) : (
          extraBets.map((bet: any) => (
          <ExtraBetCard 
            key={bet.id} 
            bet={bet} 
            isExpired={new Date(bet.deadline) < new Date()} 
            onSavePrediction={(prediction) => console.log('Saved prediction:', prediction)} 
          />          
        ))
        )}
      </div>
    </div>
  );
};