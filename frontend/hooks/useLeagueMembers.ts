import { useState, useEffect } from 'react';
import { getLeagueMembers } from '../services/api';

export interface MemberWithRank {
  id: string;
  name: string;
  points: number;
  rank: number;
}

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