import { useState, useEffect } from 'react';

// Mock-typer
type League = {
  id: string;
  name: string;
  tournamentId: string;
  membersCount: number;
  joinCode: string;
};

type Tournament = {
  id: string;
  name: string;
  startDate: string;
  endDate: string;
};

// Mock-funktioner istället för riktiga API-anrop
const getMyLeagues = async (): Promise<League[]> => {
  return [
    { id: 'l1', name: 'Mockliga 1', tournamentId: 't1', membersCount: 5, joinCode: 'ABC123' },
    { id: 'l2', name: 'Mockliga 2', tournamentId: 't1', membersCount: 3, joinCode: 'DEF456' },
  ];
};

const getTournaments = async (): Promise<Tournament[]> => {
  return [
    { id: 't1', name: 'Mockturnering 1', startDate: '2026-02-01', endDate: '2026-02-28' },
    { id: 't2', name: 'Mockturnering 2', startDate: '2026-03-01', endDate: '2026-03-31' },
  ];
};

const createLeague = async (name: string, tournamentId: string): Promise<League> => {
  return {
    id: `l${Math.floor(Math.random() * 1000)}`,
    name,
    tournamentId,
    membersCount: 1,
    joinCode: 'XYZ789',
  };
};

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