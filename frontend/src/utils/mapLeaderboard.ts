import type { LeaderboardEntryBackendDto, LeaderboardEntryDto } from '../types/leagueTypes';

export const mapLeaderboard = (
  entries: LeaderboardEntryBackendDto[]
): LeaderboardEntryDto[] => {
  const sorted = [...entries].sort((a, b) => (b.totalPoints ?? 0) - (a.totalPoints ?? 0));

  const result: LeaderboardEntryDto[] = [];
  let currentRank = 1;
  let previousPoints: number | null = null;

  sorted.forEach((entry, index) => {
    const points = entry.totalPoints ?? 0;

    if (previousPoints !== null && points < previousPoints) {
      currentRank = index + 1;
    }

    result.push({
      userId: entry.userId,
      username: entry.username,
      points: points,
      rank: currentRank,
    });

    previousPoints = points;
  });

  return result;
};
