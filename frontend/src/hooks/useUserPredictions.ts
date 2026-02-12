import { useState, useEffect } from 'react';
import { getUserPredictions } from '../services/predictionService';
import { MatchPredictionDto } from '../types/matchTypes';


export const useUserPredictions = () => {
const [predictions, setPredictions] = useState<MatchPredictionDto[]>([]);
const [loading, setLoading] = useState(true);
const [error, setError] = useState<string | null>(null);


useEffect(() => {
const fetchPredictions = async () => {
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
};

fetchPredictions();
}, []);

return { predictions, loading, error, };
};