import { CreateMatchDto, SetMatchResultDto } from "../types/matchTypes";
import { Match, Competitor } from "../types/matchTypes";
import { MatchStatusEnum } from "../types/enums/matchEnums";
import { api } from "./apiClient";


export const createMatch = async (matchData: CreateMatchDto): Promise<Match> => {
  try {
    const response = await api.post<Match>('/admin/matches', matchData);
    return response.data;
  } catch (err: any) {
    console.error('Kunde inte skapa match i servicen:', err);
    throw new Error(err.response?.data?.message || 'Fel vid skapande av match');
  }
};

export const setMatchResult = async (
  matchId: number,
  dto: SetMatchResultDto
): Promise<Match> => {
  const response = await api.put<Match>(`/admin/matches/${matchId}/result`, dto);
  return response.data;
};

export const getFilteredCompetitors = async (
  tournamentId: number,
  groupId?: number | null,
  searchTerm?: string
): Promise<Competitor[]> => {
  try {
    const response = await api.get<Competitor[]>('/admin/matches/competitors', {
      params: {
        tournamentId,
        groupId: groupId || undefined,
        searchTerm: searchTerm || undefined,
      },
    });
    return response.data;
  } catch (err: any) {
    console.error('Kunde inte hämta deltagare i servicen:', err);
    throw new Error(err.response?.data?.message || 'Fel vid hämtning av lag');
  }
};

export const updateMatchStatus = async (
  matchId: number,
  status: MatchStatusEnum
): Promise<Match> => {
  try {
    const response = await api.put<Match>(`/admin/matches/${matchId}/update-status`, null, {
      params: { status },
    });
    return response.data;
  } catch (err: any) {
    console.error('Kunde inte uppdatera matchstatus i servicen:', err);
    throw err; 
  }
};