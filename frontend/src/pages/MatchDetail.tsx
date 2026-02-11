import { useParams, useNavigate } from 'react-router-dom';
import { useMatchById } from '../hooks/useMatchById';  
import { useCreatePrediction } from '../hooks/useCreatePrediction';
import { MatchStatusEnum } from '../types/enums/matchEnums';
import { BackLink } from '../components/match/BackLink';
import { MatchHeader } from '../components/match/MatchHeader';
import { MatchPredictionForm } from '../components/match/MatchPredictionForm';
import { PredictionResult } from '../components/match/PredictionResult';
import { LockedMessage } from '../components/match/LockedMessage';

export const MatchDetail = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const { match, loading, error } = useMatchById(Number(id));
  const { handleSubmitPrediction, loading: predictionLoading, error: predictionError, prediction } = useCreatePrediction();

  if (loading || !match) return <div className="p-10 text-center">Laddar match...</div>;
  if (error) return <div className="p-10 text-center text-red-500">{error}</div>;
  const isLocked = match.status !== MatchStatusEnum.Scheduled && new Date(match.startTime) < new Date();

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
              homeTeamName={match.homeCompetitorName || ''}
              awayTeamName={match.awayCompetitorName || ''}
              isSubmitting={predictionLoading}
              showSuccess={prediction ? true : false}
              hasExistingPrediction={!!prediction}
              onSubmit={(e) => {
                e.preventDefault();
                handleSubmitPrediction(match.matchId, match.scoreHome ?? 0, match.scoreAway ?? 0);
              }}
              onCancel={() => navigate(`/tournaments/${match.tournamentId}`)}
            />
          )}
        </div>
      </div>
    </div>
  );
};