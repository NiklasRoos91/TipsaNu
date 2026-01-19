import { useState, useEffect } from 'react';
import { League, Tournament } from '../types/types';
import { getMyLeagues, getTournaments, createLeague } from '../services/api';

export const useLeagues = () => {
  const [leagues, setLeagues] = useState<League[]>([]);
  const [tournaments, setTournaments] = useState<Tournament[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [showCreate, setShowCreate] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const [leaguesData, tournamentsData] = await Promise.all([
          getMyLeagues(),
          getTournaments(),
        ]);
        setLeagues(leaguesData);
        setTournaments(tournamentsData);
      } catch (err) {
        setError('Kunde inte hämta data');
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  const handleCreateLeague = async (name: string) => {
    try {
      // In the general league list, we default to the first available tournament or a fallback
      const tournamentId = tournaments[0]?.id || 't1';
      const newLeague = await createLeague(name, tournamentId);
      setLeagues((prev) => [...prev, newLeague]);
      setShowCreate(false);
      return true;
    } catch (err) {
      console.error(err);
      return false;
    }
  };

  const toggleCreate = () => setShowCreate((prev) => !prev);

  return {
    leagues,
    loading,
    error,
    showCreate,
    toggleCreate,
    handleCreateLeague,
  };
};