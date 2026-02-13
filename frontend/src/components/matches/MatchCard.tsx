import React, { useState, useEffect } from 'react';
import {  ChevronDown, ChevronUp } from 'lucide-react';
import { Match } from '../../types/matchTypes'; 
import { MatchStatusEnum } from '../../types/enums/matchEnums';
import { useCreatePrediction  } from '../../hooks/useCreatePrediction';
import { MatchPredictionForm } from './MatchPredictionForm';
import { useSetMatchResult } from '../../hooks/useSetMatchResult';
import { useAuth } from '../../hooks/useAuth';
import { MatchResultForm } from './MatchResultForm';
import { PredictionResultBadge } from './PredictionResultBadge';

interface MatchCardProps {
  match: Match;
  prediction: { homeScore: number; awayScore: number } | null;
  groups: { name: string, groupId: number }[]; 
}

export const MatchCard: React.FC<MatchCardProps> = ({ match, prediction: initialPrediction, groups }) => {
  const [isExpanded, setIsExpanded] = useState(false);
  const [homePred, setHomePred] = useState<number | ''>(initialPrediction?.homeScore ?? 0);
  const [awayPred, setAwayPred] = useState<number | ''>(initialPrediction?.awayScore ?? 0);
  const [showSuccess, setShowSuccess] = useState(false);
  const { handleSubmitPrediction, loading: isSubmitting, prediction: newPrediction } = useCreatePrediction();
  const { handleSubmitResult, loading: isSubmittingResult, updatedMatch } = useSetMatchResult();
  const { isAdmin } = useAuth();const [homeScoreLocal, setHomeScoreLocal] = useState<number | null>(match.scoreHome);
  const [awayScoreLocal, setAwayScoreLocal] = useState<number | null>(match.scoreAway);

  

  const InProgress = match.status === MatchStatusEnum.InProgress;
  const isFinished = match.status === MatchStatusEnum.Finished;
  const isScheduled = match.status === MatchStatusEnum.Scheduled;
  const isLocked = !isFinished && match.status !== MatchStatusEnum.Scheduled && new Date(match.startTime) < new Date();

  const groupName = groups.find(group => group.groupId === match.groupId)?.name ?? "Okänd grupp";

  useEffect(() => {
    if (initialPrediction) {
      setHomePred(initialPrediction.homeScore);
      setAwayPred(initialPrediction.awayScore);
    } else {
      setHomePred(0);
      setAwayPred(0);
    }
  }, [initialPrediction]);

    useEffect(() => {
    if (newPrediction) {
      setHomePred(newPrediction.predictedHomeScore);
      setAwayPred(newPrediction.predictedAwayScore);
      setShowSuccess(true);
      setTimeout(() => {
      setShowSuccess(false);
      }, 2000);
    }
  }, [newPrediction]);

  useEffect(() => {
  if (updatedMatch) {
    setHomeScoreLocal(updatedMatch.scoreHome);
    setAwayScoreLocal(updatedMatch.scoreAway);
  }
}, [updatedMatch]);

const handlePredictionSubmit = async (homeScore: number, awayScore: number) => {
  if (isSubmitting || isLocked || isFinished) return;
  await handleSubmitPrediction(match.matchId, homeScore, awayScore);
};

  const handleCancel = () => {
    setHomePred(initialPrediction?.homeScore ?? 0);
    setAwayPred(initialPrediction?.awayScore ?? 0);
    setIsExpanded(false);
  };

  return (
    <div 
      className={`bg-white rounded-xl shadow-sm border transition-all overflow-hidden ${
        isExpanded ? 'border-accent ring-1 ring-accent/20 shadow-md' : 'border-slate-200 hover:border-accent hover:shadow-md'
      }`}
    >
      {/* Main Card Header (Clickable) */}
      <div 
        onClick={() => !isFinished && !isLocked && setIsExpanded(!isExpanded)}
        className={`p-4 cursor-pointer select-none transition-colors ${isFinished || isLocked ? 'cursor-default' : 'hover:bg-slate-50/50'}`}
      >
        <div className="flex justify-between items-center mb-3 text-xs font-bold tracking-wider uppercase">
          <span className="text-slate-400">{groupName}</span>
          <div className="flex items-center gap-3">
            <span className={`${InProgress ? 'text-red-500 animate-pulse' : 'text-slate-400'}`}>
              {InProgress ? 'LIVE' : isFinished ? 'AVSLUTAD' : new Date(match.startTime).toLocaleDateString('sv-SE', { day: 'numeric', month: 'short' })}
            </span>
            {!isFinished && !isLocked && (
              isExpanded ? <ChevronUp size={16} className="text-accent" /> : <ChevronDown size={16} className="text-slate-300" />
            )}
          </div>
        </div>

        <div className="flex items-center justify-between">
          <div className="flex items-center gap-3 w-1/3">
            <img src={match.homeCompetitorName} alt="" className="w-8 h-6 object-cover rounded shadow-sm border border-slate-100" />
            <span className="font-semibold text-primary truncate">{match.homeCompetitorName}</span>
          </div>

          <div className="flex flex-col items-center justify-center w-1/3">
            <div className="text-xl font-black text-slate-800 font-mono">
              {homeScoreLocal == null || awayScoreLocal == null ? 'VS' : `${homeScoreLocal} - ${awayScoreLocal}`}
            </div>

            <div className="mt-2">
              <PredictionResultBadge
                prediction={
                  newPrediction
                    ? newPrediction
                    : initialPrediction
                    ? {
                        predictedHomeScore: initialPrediction.homeScore,
                        predictedAwayScore: initialPrediction.awayScore,
                      }
                    : null
                }
                matchResult={{ scoreHome: homeScoreLocal, scoreAway: awayScoreLocal }}
                matchStatus={match.status}
              />
            </div>
          </div>
          <div className="flex items-center gap-3 w-1/3 justify-end text-right">
            <span className="font-semibold text-primary truncate">{match.awayCompetitorName}</span>
            <img src={match.awayCompetitorName} alt="" className="w-8 h-6 object-cover rounded shadow-sm border border-slate-100" />
          </div>
        </div>
      </div>

      {/* Expanded Area */}
      {isExpanded && (
        <div className="border-t border-slate-100 bg-slate-50/50 p-6 animate-fade-in">
          <div className="max-w-xs mx-auto">
            <>
              <h4 className="text-center text-sm font-bold text-slate-500 uppercase tracking-widest mb-6">
                Ditt tips för matchen
              </h4>
              <MatchPredictionForm 
                match={{
                  homeCompetitorName: match.homeCompetitorName,
                  awayCompetitorName: match.awayCompetitorName
                }}
                prediction={initialPrediction ? {
                  matchId: match.matchId,
                  predictedHomeScore: initialPrediction.homeScore,
                    predictedAwayScore: initialPrediction.awayScore,
                    pointsAwarded: 0,
                  submittedAt: new Date().toISOString()
                } : undefined}
                onSubmit={handlePredictionSubmit}
                onCancel={handleCancel}
              />
            </>
            
            {/*
              TODO: Only show the match result form if the match has been played.
              Right now it always shows for admins, but later we can add a condition
              using `isFinished` or check `match.startTime` to block input before the match starts.
            */}
            {/* Admin: match result */}
            {isAdmin && (
              <>
                <h4 className="text-center text-sm font-bold text-slate-500 uppercase tracking-widest mb-6">
                  Mata in matchresultat
                </h4>
                <MatchResultForm 
                  match={{
                    homeCompetitorName: match.homeCompetitorName,
                    awayCompetitorName: match.awayCompetitorName
                  }}
                  onSubmit={async (homeScore, awayScore) => {
                    await handleSubmitResult(match.matchId, homeScore, awayScore);
                    setHomeScoreLocal(homeScore);
                    setAwayScoreLocal(awayScore);
                  }}
                  onCancel={handleCancel}
                />
              </>
            )}
          </div>
        </div>
      )}
    </div>
  );
};