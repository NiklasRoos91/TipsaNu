import { useState, useEffect, useCallback } from 'react';
import { getMatch, getMyPrediction, submitPrediction as apiSubmitPrediction } from '../services/api';
import { Match, Prediction } from '../types';

export const useMatchDetail = (matchId?: string) => {
  const [match, setMatch] = useState<Match | undefined>();
  const [prediction, setPrediction] = useState<Prediction | null>(null);
  const [homePred, setHomePred] = useState<number | ''>(0);
  const [awayPred, setAwayPred] = useState<number | ''>(0);
  const [loading, setLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [showSuccess, setShowSuccess] = useState(false);

  const fetchMatchData = useCallback(async (id: string) => {
    setLoading(true);
    try {
      const [m, p] = await Promise.all([getMatch(id), getMyPrediction(id)]);
      setMatch(m);
      setPrediction(p);
      if (p) {
        setHomePred(p.homeScore);
        setAwayPred(p.awayScore);
      } else {
        setHomePred(0);
        setAwayPred(0);
      }
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    if (matchId) {
      fetchMatchData(matchId);
    }
  }, [matchId, fetchMatchData]);

  const updateHomePred = (value: number | '') => setHomePred(value);
  const updateAwayPred = (value: number | '') => setAwayPred(value);

  const submitPrediction = async () => {
    if (!match || homePred === '' || awayPred === '') return;
    setIsSubmitting(true);
    try {
      await apiSubmitPrediction(match.id, Number(homePred), Number(awayPred));
      const updated = await getMyPrediction(match.id);
      setPrediction(updated);
      setShowSuccess(true);
      setTimeout(() => setShowSuccess(false), 3000);
    } finally {
      setIsSubmitting(false);
    }
  };

  return {
    match,
    prediction,
    homePred,
    awayPred,
    loading,
    isSubmitting,
    showSuccess,
    updateHomePred,
    updateAwayPred,
    submitPrediction
  };
};