
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