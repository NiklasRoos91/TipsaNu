import { api } from './apiClient';
import type { LeagueDto, LeagueWithLeaderboardDto, CreateLeagueDto, CreatedLeagueWithMemberDto, JoinLeagueDto, LeagueMemberDto } from '../types/leagueTypes';

export const getMyLeaguesInTournament = async (
  tournamentId: number
): Promise<LeagueDto[]> => {
  try {
    const response = await api.get<LeagueDto[]>(
      `/leagues/${tournamentId}/leagues/me`
    );
    return response.data;
  } catch (err: any) {
    console.error(`Error fetching my leagues for tournament ${tournamentId}`, err);
    throw new Error(err.response?.data?.message || 'Kunde inte hämta dina ligor');
  }
};

export const getLeagueById = async (leagueId: number): Promise<LeagueWithLeaderboardDto> => {
  try {
    const response = await api.get<LeagueWithLeaderboardDto>(`/leagues/${leagueId}`);
    return response.data;
  } catch (err: any) {
    console.error(`Error fetching league ${leagueId}`, err);
    throw new Error(err.response?.data?.message || 'Fel vid hämtning av liga');
  }
};

export const createLeague = async (data: CreateLeagueDto): Promise<CreatedLeagueWithMemberDto> => {
  const response = await api.post('/leagues', data);
  return response.data;
};

export const joinLeague = async (
  tournamentId: number,
  dto: JoinLeagueDto
): Promise<LeagueMemberDto> => {
  try {
    const response = await api.post<LeagueMemberDto>(`/leagues/${tournamentId}/join`, dto);
    return response.data;
  } catch (err: any) {
    console.error(`Error joining league in tournament ${tournamentId}`, err);
    throw new Error(err.response?.data?.message || 'Kunde inte gå med i ligan');
  }
};