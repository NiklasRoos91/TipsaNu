
export interface User {
  id: string;
  username: string;
  email: string;
  displayName: string;
  avatarUrl?: string;
  points: number;
  bio?: string;
}

export enum MatchStatus {
  SCHEDULED = 'SCHEDULED',
  LIVE = 'LIVE',
  FINISHED = 'FINISHED'
}

export interface Team {
  id: string;
  name: string;
  flagUrl: string;
}

export interface Match {
  id: string;
  tournamentId: string;
  homeTeam: Team;
  awayTeam: Team;
  startTime: string; // ISO date string
  status: MatchStatus;
  homeScore?: number;
  awayScore?: number;
  venue?: string;
  group?: string;
}

export interface Tournament {
  id: string;
  name: string;
  startDate: string;
  endDate: string;
  status: 'UPCOMING' | 'ACTIVE' | 'COMPLETED';
  bannerUrl: string;
}

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

export interface Prediction {
  id: string;
  userId: string;
  matchId: string;
  homeScore: number;
  awayScore: number;
  pointsAwarded?: number;
}

export interface League {
  id: string;
  name: string;
  tournamentId: string; // Associated tournament
  ownerId: string;
  code: string; // Invitation code
  membersCount: number;
  description?: string;
}

export interface LeagueMember {
  userId: string;
  username: string;
  points: number;
  rank: number;
}

export interface Post {
  id: string;
  leagueId: string;
  userId: string;
  username: string;
  content: string;
  createdAt: string;
  likes: number;
}

export interface Notification {
  id: string;
  userId: string;
  type: 'POINTS_AWARDED' | 'MATCH_STARTED' | 'EXTRABET_RESULT' | 'LEAGUE_INVITE' | 'PREDICTION_MADE';
  title: string;
  message: string;
  read: boolean;
  createdAt: string;
  link?: string;
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

export interface GroupStanding {
  rank: number;
  competitor: Team;
  played: number;
  wins: number;
  draws: number;
  losses: number;
  goalsFor: number;
  goalsAgainst: number;
  goalDifference: number;
  points: number;
}
