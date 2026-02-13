// --- League (frontend-version) ---
export interface League {
  leagueId: number;
  tournamentId: number;
  name: string;
  description: string;
  adminUserId: number;
  invitationCode: string;
  createdAt: string;
  maxMembers: number;
}

export interface CreateLeagueDto {
  tournamentId: number;
  name: string;
  description: string;
  invitationCode?: string;
  maxMembers: number;
}

export interface JoinLeagueDto {
  invitationCode: string;
}

export interface LeaderboardEntryDto {
  userId: number;
  username: string;
  points: number;
  rank: number;
}

export interface LeagueWithLeaderboardDto {
  leagueId: number;
  name: string;
  description: string;
  invitationCode: string;
  maxMembers: number;
  currentMembers: number;
  adminUserId: number;
  adminUsername: string;
  leaderboard: LeaderboardEntryDto[];
}

export interface LeagueDto {
  leagueId: number;
  tournamentId: number;
  name: string;
  description: string;
  invitationCode: string;
  adminUserId: number;
  maxMembers?: number;
}

export interface LeagueMemberDto {
  leagueMemberId: number;
  leagueId: number;
  userId: number;
  joinedAt: string; // ISO string
}

export interface CreatedLeagueWithMemberDto {
  leagueDto: LeagueDto;
  leagueMemberDto: LeagueMemberDto;
}
