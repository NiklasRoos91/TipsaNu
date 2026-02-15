export interface GroupStanding {
  rank: number;

  competitor: {
    id: number;
    name: string;
    flagUrl: string;
  };

  played: number;
  wins: number;
  draws: number;
  losses: number;

  goalsFor: number;
  goalsAgainst: number;
  goalDifference: number;

  points: number;
}

export const MOCK_GROUP_STANDINGS: GroupStanding[] = [
  {
    rank: 1,
    competitor: { id: 1, name: "Sverige", flagUrl: "https://flagcdn.com/w40/se.png" },
    played: 3,
    wins: 2,
    draws: 1,
    losses: 0,
    goalsFor: 5,
    goalsAgainst: 2,
    goalDifference: 3,
    points: 7,
  },
  {
    rank: 2,
    competitor: { id: 2, name: "Tyskland", flagUrl: "https://flagcdn.com/w40/de.png" },
    played: 3,
    wins: 2,
    draws: 0,
    losses: 1,
    goalsFor: 6,
    goalsAgainst: 3,
    goalDifference: 3,
    points: 6,
  },
  {
    rank: 3,
    competitor: { id: 3, name: "Spanien", flagUrl: "https://flagcdn.com/w40/es.png" },
    played: 3,
    wins: 1,
    draws: 0,
    losses: 2,
    goalsFor: 4,
    goalsAgainst: 5,
    goalDifference: -1,
    points: 3,
  },
  {
    rank: 4,
    competitor: { id: 4, name: "Polen", flagUrl: "https://flagcdn.com/w40/pl.png" },
    played: 3,
    wins: 0,
    draws: 1,
    losses: 2,
    goalsFor: 2,
    goalsAgainst: 7,
    goalDifference: -5,
    points: 1,
  },
];
