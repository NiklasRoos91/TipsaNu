import { useState } from 'react';
import { createPrediction } from '../services/matchService';
import { CreateMyPredictionRequestDto, MatchPredictionDto } from '../types/matchTypes';

export const useCreatePrediction = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [prediction, setPrediction] = useState<MatchPredictionDto | null>(null);

  const handleSubmitPrediction = async (matchId: number, predictedHomeScore: number, predictedAwayScore: number) => {
    const predictionData: CreateMyPredictionRequestDto = {
      predictedHomeScore,
      predictedAwayScore
    };

    setLoading(true);
    setError(null);

    try {
      const result: MatchPredictionDto = await createPrediction(matchId, predictionData);
      setPrediction(result);
    } catch (error: any) {
      setError(error.message || 'Något gick fel vid skapandet av tippningen');
    } finally {
      setLoading(false);
    }
  };

  return { handleSubmitPrediction, loading, error, prediction };
};
