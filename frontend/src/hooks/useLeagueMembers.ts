import { useState, useEffect } from 'react';

export interface MemberWithRank {
  id: string;
  name: string;
  points: number;
  rank: number;
}

// Mock-typ för medlem
export interface MemberWithRank {
  id: string;
  name: string;
  points: number;
  rank: number;
}

// Mock-funktion som ersätter getLeagueMembers
const getLeagueMembers = async (id: string) => {
  return new Promise<MemberWithRank[]>(resolve => {
    setTimeout(() => {
      resolve([
        { id: '1', name: 'Alice', points: 25, rank: 0 },
        { id: '2', name: 'Bob', points: 30, rank: 0 },
        { id: '3', name: 'Charlie', points: 20, rank: 0 }
      ]);
    }, 500);
  });
};

export const useLeagueMembers = (id?: string) => {
  const [members, setMembers] = useState<MemberWithRank[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!id) return;
    setLoading(true);
    getLeagueMembers(id).then((membersData) => {
      const sorted = [...membersData].sort((a, b) => b.points - a.points);
      
      const ranked: MemberWithRank[] = sorted.map((member) => ({
        ...member,
        rank: 0
      }));
      
      let currentRank = 1;
      for (let i = 0; i < ranked.length; i++) {
        if (i > 0 && ranked[i].points < ranked[i - 1].points) {
          currentRank = i + 1;
        }
        ranked[i].rank = currentRank;
      }

      setMembers(ranked);
      setLoading(false);
    });
  }, [id]);

  return { members, loading };
};