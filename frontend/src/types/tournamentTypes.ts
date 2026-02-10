import { TournamentStatusEnum, MatchStatusEnum } from './enums/tournamentEnums';

// Tournament
export interface Tournament {
  TournamentId: number;
  Name: string;
  StartsAt: string;
  EndDate?: string; // <-- lägg till
  Status: TournamentStatusEnum;
  BannerUrl?: string;
}

// Group
export interface Group {
  groupId: number;
  tournamentId: number;
  name: string;
}

// Match
export interface Match {
  matchId: number;
  tournamentId: number;
  groupId: number;
  matchType: number;
  roundNumber: number;
  startTime: string; // ISO-datum
  predictionDeadline?: string | null;
  homeCompetitorId: number;
  homeCompetitorName: string;
  awayCompetitorId: number;
  awayCompetitorName: string;
  scoreHome?: number | null;
  scoreAway?: number | null;
  winnerCompetitorId?: number | null;
  winnerCompetitorName?: string | null;
  status: MatchStatusEnum;
}

// Group Standing
export interface GroupStanding {
  competitorId: number;
  competitorName: string;
  played: number;
  won: number;
  draw: number;
  lost: number;
  goalsFor: number;
  goalsAgainst: number;
  points: number;
  rank: number;
}
