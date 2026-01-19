import React from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useMatchDetail } from '../hooks/useMatchDetail';
import { MatchStatus } from '../types/types';
import { BackLink } from '../components/match/BackLink';
import { MatchHeader } from '../components/match/MatchHeader';
import { MatchPredictionForm } from '../components/match/MatchPredictionForm';
import { PredictionResult } from '../components/match/PredictionResult';
import { LockedMessage } from '../components/match/LockedMessage';

export const MatchDetail = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const {
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
  } = useMatchDetail(id);

  if (loading || !match) return <div className="p-10 text-center">Laddar match...</div>;

  const isLocked = match.status !== MatchStatus.SCHEDULED && new Date(match.startTime) < new Date();

  return (
    <div className="max-w-2xl mx-auto">
      <BackLink tournamentId={match.tournamentId} />

      <div className="bg-white rounded-2xl shadow-lg border border-slate-200 overflow-hidden">
        <MatchHeader match={match} />

        <div className="p-8 relative">
          <h3 className="text-xl font-bold text-center mb-6 text-primary">Ditt Tips</h3>
          
          {isLocked ? (
            prediction ? (
              <PredictionResult prediction={prediction} />
            ) : (
              <LockedMessage />
            )
          ) : (
            <MatchPredictionForm 
              homeTeamName={match.homeTeam.name}
              awayTeamName={match.awayTeam.name}
              homePred={homePred}
              awayPred={awayPred}
              isSubmitting={isSubmitting}
              showSuccess={showSuccess}
              hasExistingPrediction={!!prediction}
              onHomeChange={updateHomePred}
              onAwayChange={updateAwayPred}
              onCancel={() => navigate(`/tournaments/${match.tournamentId}`)}
              onSubmit={(e) => {
                e.preventDefault();
                submitPrediction();
              }}
            />
          )}
        </div>
      </div>
    </div>
  );
};