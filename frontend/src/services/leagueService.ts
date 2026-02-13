import { api } from './apiClient';
import type { LeagueDto, LeagueWithLeaderboardDto } from '../types/leagueTypes';

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