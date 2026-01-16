
import React from 'react';
import { useLeagues } from '../hooks/useLeagues';
import { LeagueCard } from '../components/league/LeagueCard';
import { CreateLeagueForm } from '../components/league/CreateLeagueForm';
import { LayoutGrid, Plus } from 'lucide-react';
import { ActionButton } from '../components/common/ActionButton';

export const LeagueList = () => {
  const {
    leagues,
    loading,
    error,
    showCreate,
    toggleCreate,
    handleCreateLeague,
  } = useLeagues();

  if (loading) {
    return (
      <div className="max-w-6xl mx-auto p-10 text-center text-slate-400 animate-pulse">
        Laddar dina ligor...
      </div>
    );
  }

  if (error) {
    return (
      <div className="max-w-6xl mx-auto p-10 text-center text-red-500 font-bold">
        {error}
      </div>
    );
  }

  return (
    <div className="max-w-6xl mx-auto pb-12">
      <div className="flex flex-col md:flex-row justify-between items-start md:items-center gap-6 mb-10 px-4 md:px-0">
        <div>
          <h2 className="text-3xl font-bold text-primary flex items-center gap-3">
            <LayoutGrid className="text-accent" size={32} />
            Mina Ligor
          </h2>
          <p className="text-slate-500 mt-1 font-medium">Ligor du skapat eller gått med i.</p>
        </div>
        
        <div className="w-full md:w-auto">
          <ActionButton 
            label="Skapa ny Liga" 
            onClick={toggleCreate} 
            isActive={showCreate}
            icon={Plus}
          />
        </div>
      </div>

      {showCreate && (
        <div className="mb-10 animate-fade-in max-w-2xl px-4 md:px-0">
          <CreateLeagueForm 
            onSubmit={handleCreateLeague} 
            onCancel={toggleCreate} 
          />
        </div>
      )}

      {leagues.length > 0 ? (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 px-4 md:px-0">
          {leagues.map((league) => (
            <LeagueCard key={league.id} league={league} />
          ))}
        </div>
      ) : (
        <div className="mx-4 md:mx-0 text-center p-20 bg-white rounded-3xl border-2 border-dashed border-slate-200 text-slate-400 shadow-sm">
          <div className="flex flex-col items-center gap-6">
            <div className="w-20 h-20 bg-slate-50 rounded-full flex items-center justify-center">
              <LayoutGrid size={40} className="opacity-20" />
            </div>
            <div className="space-y-2">
              <p className="text-xl font-bold text-slate-600">Du är inte med i några ligor ännu</p>
              <p className="text-slate-400 max-w-xs mx-auto">Skapa en egen liga eller be en vän om en inbjudningskod för att börja tävla!</p>
            </div>
            <button
              onClick={toggleCreate}
              className="mt-2 bg-accent/10 text-accent px-6 py-2 rounded-full font-bold hover:bg-accent hover:text-white transition-all"
            >
              Skapa din första liga nu
            </button>
          </div>
        </div>
      )}
    </div>
  );
};
