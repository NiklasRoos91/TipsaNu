import { useState, useEffect, useCallback  } from 'react';
import { getUserPredictions } from '../services/predictionService';
import { MatchPredictionDto } from '../types/matchTypes';


export const useUserPredictions = () => {
  const [predictions, setPredictions] = useState<MatchPredictionDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchPredictions = useCallback (async () => {
  setLoading(true);
  setError(null);
  try {
    const data = await getUserPredictions();
    setPredictions(data);
  } catch (err) {
    setError('Kunde inte hämta dina tippningar.');
  } finally {
    setLoading(false);
  }
}, []);

useEffect(() => {
    fetchPredictions();
}, [fetchPredictions]);

return { predictions, loading, error, refreshPredictions: fetchPredictions };
};