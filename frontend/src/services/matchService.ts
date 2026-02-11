import { Match, CreateMyPredictionRequestDto, MatchPredictionDto } from '../types/matchTypes';
import { api } from './apiClient';

export const getMatchById = async (matchId: number): Promise<Match> => {
  try {
    const response = await api.get<Match>(`/matches/${matchId}`);
    return response.data;
  } catch (error) {
    console.error(`Error fetching details for match ${matchId}`, error);
    throw new Error(error.response?.data?.message || 'Error fetching match details');
  }
};

export const createPrediction = async (
  matchId: number, 
  prediction: CreateMyPredictionRequestDto
): Promise<MatchPredictionDto> => {
  try {
    const response = await api.post<MatchPredictionDto>(`/matches/${matchId}/predictions/mine`, prediction);
    return response.data;
  } catch (err: any) {
    console.error(`Kunde inte skapa tippning för match ${matchId}`, err);
    throw new Error(err.response?.data?.message || 'Fel vid skapande av tippning');
  }
}