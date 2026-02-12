import React, { useState, useEffect } from 'react';
import { Check, ChevronDown, ChevronUp } from 'lucide-react';
import { Match } from '../../types/tournamentTypes'; 
import { MatchStatusEnum } from '../../types/enums/matchEnums';
import { useMatchById } from '../../hooks/useMatchById';
import { useCreatePrediction  } from '../../hooks/useCreatePrediction';
import { MatchPredictionForm } from './MatchPredictionForm';

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
  const { match: fetchedMatch, loading, error } = useMatchById(match.matchId);
  const { handleSubmitPrediction, loading: isSubmitting, prediction: newPrediction } = useCreatePrediction();


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

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (homePred === '' || awayPred === '' || isSubmitting || isLocked || isFinished) return;

    await handleSubmitPrediction(match.matchId, Number(homePred), Number(awayPred));
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
              {match.status === MatchStatusEnum.Scheduled || match.scoreHome == null || match.scoreAway == null 
                ? 'VS' 
                : `${match.scoreHome} - ${match.scoreAway}`}
            </div>

            <div className="mt-2">
              {(newPrediction || initialPrediction) ? (
                <div className="flex items-center gap-1 text-[10px] font-bold text-emerald-600 bg-emerald-50 px-2 py-0.5 rounded-full border border-emerald-100">
                  <Check size={10} />
                  <span>
                    { (newPrediction?.predictedHomeScore ?? initialPrediction?.homeScore) }-
                    { (newPrediction?.predictedAwayScore ?? initialPrediction?.awayScore) }
                  </span>
                </div>
              ) : !isFinished && !isLocked ? (
                <div className={`text-[10px] font-bold px-2 py-0.5 rounded-full transition-colors ${
                  isExpanded ? 'bg-accent text-white' : 'text-slate-400 bg-slate-100'
                }`}>
                  TIPPA
                </div>
              ) : (
                <div className="text-[10px] font-bold text-slate-300 bg-slate-50 px-2 py-0.5 rounded-full uppercase italic">
                  Ej tippad
                </div>
              )}
            </div>
          </div>



          <div className="flex items-center gap-3 w-1/3 justify-end text-right">
            <span className="font-semibold text-primary truncate">{match.awayCompetitorName}</span>
            <img src={match.awayCompetitorName} alt="" className="w-8 h-6 object-cover rounded shadow-sm border border-slate-100" />
          </div>
        </div>
      </div>

      {/* Expanded Prediction Area */}
      {isExpanded && (
        <div className="border-t border-slate-100 bg-slate-50/50 p-6 animate-fade-in">
          <div className="max-w-xs mx-auto">
            <h4 className="text-center text-sm font-bold text-slate-500 uppercase tracking-widest mb-6">
              Ditt tips för matchen
            </h4>
            <MatchPredictionForm 
              homeTeamName={match.homeCompetitorName}
              awayTeamName={match.awayCompetitorName}
              onSubmit={handleSubmit}
              onCancel={handleCancel}
              isSubmitting={isSubmitting}
              showSuccess={showSuccess}
              hasExistingPrediction={!!newPrediction}
              homePred={homePred}
              awayPred={awayPred}
              onHomePredChange={setHomePred}
              onAwayPredChange={setAwayPred}
            />
          </div>
        </div>
      )}
    </div>
  );
};