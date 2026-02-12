import { api } from './apiClient'; 
import { Prediction } from '../types/matchTypes';

export const getUserPredictions = async (): Promise<Prediction[]> => {
  try {
    const response = await api.get(`/predictions/me`);
    return response.data;
  } catch (error) {
    console.error('Error fetching user predictions:', error);
    throw error;
  }
};
