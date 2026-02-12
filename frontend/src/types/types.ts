
export enum TiebreakerCriterion {
  HeadToHead = 'Inbördes möten',
  GoalDifference = 'Målskillnad',
  GoalsScored = 'Gjorda mål',
  FairPlay = 'Fair Play',
  Random = 'Lottning'
}

export interface TournamentTiebreaker {
  criterion: TiebreakerCriterion;
  priority: number;
}

export interface PointRule {
  id: string;
  name: string;
  pointsForExactScore: number;
  pointsForCorrectGoalDifference: number;
  pointsForCorrectWinner: number;
}

export interface TournamentTemplate {
  id: string;
  name: string;
  description: string;
  totalGroups: number;
  advancingPerGroup: number;
  allowsBestThird: boolean;
  pointRules: PointRule[];
  tiebreakers: TournamentTiebreaker[];
}

export interface ExtraBet {
  id: string;
  tournamentId: string;
  question: string; // Acts as "name"
  description?: string;
  allowedValues?: string[]; // Predefined options (suggestions)
  requiresValue: boolean; // If true, allows/requires free text input
  deadline: string;
  points: number;
  result?: string;
}

export interface ExtraBetPrediction {
  id: string;
  extraBetId: string;
  userId: string;
  selectedOption: string;
  createdAt: string;
}
