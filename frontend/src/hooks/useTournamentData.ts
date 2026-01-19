import { useState, useEffect } from 'react';
import { 
  getMatches, 
  getTournament, 
  getMyPredictionsForTournament, 
  getExtraBets, 
  getMyLeagues,
  getMyExtraBetPredictions
} from '../services/api';
import { Tournament, Match, Prediction, ExtraBet, League, ExtraBetPrediction } from '../types/types';

export const useTournamentData = (id?: string) => {
  const [tournament, setTournament] = useState<Tournament | undefined>();
  const [matches, setMatches] = useState<Match[]>([]);
  const [extraBets, setExtraBets] = useState<ExtraBet[]>([]);
  const [extraBetPredictions, setExtraBetPredictions] = useState<ExtraBetPrediction[]>([]);
  const [leagues, setLeagues] = useState<League[]>([]);
  const [predictions, setPredictions] = useState<Prediction[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (id) {
      setLoading(true);
      Promise.all([
        getTournament(id),
        getMatches(id),
        getMyPredictionsForTournament(id),
        getExtraBets(id),
        getMyLeagues(id),
        getMyExtraBetPredictions(id)
      ]).then(([t, m, p, eb, l, ebp]) => {
        setTournament(t);
        setMatches(m);
        setPredictions(p);
        setExtraBets(eb);
        setLeagues(l);
        setExtraBetPredictions(ebp);
        setLoading(false);
      });
    }
  }, [id]);

  const updateExtraBetPredictions = (prediction: ExtraBetPrediction) => {
    setExtraBetPredictions(prev => {
      const exists = prev.find(p => p.id === prediction.id || p.extraBetId === prediction.extraBetId);
      if (exists) {
        return prev.map(p => (p.id === prediction.id || p.extraBetId === prediction.extraBetId) ? prediction : p);
      }
      return [prediction, ...prev];
    });
  };

  return { 
    tournament, 
    matches, 
    extraBets, 
    setExtraBets, 
    extraBetPredictions,
    updateExtraBetPredictions,
    leagues, 
    setLeagues, 
    predictions, 
    loading 
  };
};