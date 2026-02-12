import{ MatchTypeEnum, MatchStatusEnum } from './enums/matchEnums';

export interface Match {
  matchId: number;
  tournamentId: number;
  groupId: number;
  matchType: MatchTypeEnum;
  roundNumber: number;
  startTime: string;
  predictionDeadline: string | null;
  homeCompetitorId: number;
  homeCompetitorName: string | null;
  awayCompetitorId: number;
  awayCompetitorName: string | null;
  scoreHome: number | null;
  scoreAway: number | null;
  winnerCompetitorId: number | null;
  winnerCompetitorName: string | null;
  status: MatchStatusEnum;
}

// Prediction
export interface Prediction {
  predictionId: number;
  userId: number;
  matchId: number;
  predictedHomeScore: number;
  predictedAwayScore: number;
  predictedWinnerId?: number;
  pointsAwarded: number;
  submittedAt: string;
}

export interface MatchPredictionDto {
  matchId: number;
  predictedHomeScore: number;
  predictedAwayScore: number;
  predictedWinnerId?: number | null;
  pointsAwarded: number;
  submittedAt: string; 

  // Match info
  homeTeamName?: string;
  awayTeamName?: string;
  matchStartTime?: string;
}

// CreateMyPredictionRequestDto
export interface CreateMyPredictionRequestDto {
  predictedHomeScore: number;
  predictedAwayScore: number;
}
