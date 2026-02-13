import { SetMatchResultDto } from "../types/matchTypes";
import { Match } from "../types/matchTypes";
import { api } from "./apiClient";

export const setMatchResult = async (
  matchId: number,
  dto: SetMatchResultDto
): Promise<Match> => {
  const response = await api.put<Match>(`/admin/matches/${matchId}/result`, dto);
  return response.data;
};