import { TournamentStatusEnum } from './enums/tournamentEnums';

// Tournament
export interface Tournament {
  tournamentId: number;
  name: string;
  startsAt: string;
  endDate?: string;
  status: TournamentStatusEnum;
  bannerUrl?: string;
}

// Group
export interface Group {
  groupId: number;
  tournamentId: number;
  name: string;
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
