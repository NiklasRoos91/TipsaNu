import { useState, useEffect } from 'react';

// Mock-typ för League
export type League = {
  id: string;
  name: string;
  tournamentId: string;
  membersCount: number;
  joinCode: string;
};

// Mock-funktion istället för API-anrop
const getLeague = async (id: string): Promise<League> => {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve({
        id,
        name: `Mockliga ${id}`,
        tournamentId: '123',
        membersCount: 10,
        joinCode: 'ABCDE'
      });
    }, 300); // simulerar liten delay
  });
};

export const useLeague = (id?: string) => {
  const [league, setLeague] = useState<League | undefined>();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!id) return;
    setLoading(true);
    getLeague(id).then((data) => {
      setLeague(data);
      setLoading(false);
    });
  }, [id]);

  return { league, loading };
};