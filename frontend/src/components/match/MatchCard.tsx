import React, { useState, useEffect } from 'react';
import { Check, ChevronDown, ChevronUp } from 'lucide-react';
import { Match, Prediction, MatchStatus } from '../../types';
import { submitPrediction as apiSubmitPrediction } from '../../services/api';
import { MatchPredictionForm } from './MatchPredictionForm';

interface MatchCardProps {
  match: Match;
  prediction?: Prediction;
}

export const MatchCard: React.FC<MatchCardProps> = ({ match, prediction: initialPrediction }) => {
  const [isExpanded, setIsExpanded] = useState(false);
  const [prediction, setPrediction] = useState<Prediction | undefined>(initialPrediction);
  const [homePred, setHomePred] = useState<number | ''>(initialPrediction?.homeScore ?? 0);
  const [awayPred, setAwayPred] = useState<number | ''>(initialPrediction?.awayScore ?? 0);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [showSuccess, setShowSuccess] = useState(false);

  const isLive = match.status === MatchStatus.LIVE;
  const isFinished = match.status === MatchStatus.FINISHED;
  const isLocked = !isFinished && match.status !== MatchStatus.SCHEDULED && new Date(match.startTime) < new Date();

  // Keep local state in sync if initialPrediction changes from parent (e.g. on bulk load)
  useEffect(() => {
    if (initialPrediction) {
      setPrediction(initialPrediction);
      setHomePred(initialPrediction.homeScore);
      setAwayPred(initialPrediction.awayScore);
    } else {
      setHomePred(0);
      setAwayPred(0);
    }
  }, [initialPrediction]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (homePred === '' || awayPred === '' || isSubmitting || isLocked || isFinished) return;

    setIsSubmitting(true);
    try {
      const result = await apiSubmitPrediction(match.id, Number(homePred), Number(awayPred));
      setPrediction(result);
      setShowSuccess(true);
      setTimeout(() => {
        setShowSuccess(false);
        setIsExpanded(false);
      }, 2000);
    } catch (error) {
      alert('Kunde inte spara tipset.');
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleCancel = () => {
    // Reset to initial values from state (which mirrors the saved prediction)
    setHomePred(prediction?.homeScore ?? 0);
    setAwayPred(prediction?.awayScore ?? 0);
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
          <span className="text-slate-400">{match.group}</span>
          <div className="flex items-center gap-3">
            <span className={`${isLive ? 'text-red-500 animate-pulse' : 'text-slate-400'}`}>
              {isLive ? 'LIVE' : isFinished ? 'AVSLUTAD' : new Date(match.startTime).toLocaleDateString('sv-SE', { day: 'numeric', month: 'short' })}
            </span>
            {!isFinished && !isLocked && (
              isExpanded ? <ChevronUp size={16} className="text-accent" /> : <ChevronDown size={16} className="text-slate-300" />
            )}
          </div>
        </div>

        <div className="flex items-center justify-between">
          <div className="flex items-center gap-3 w-1/3">
            <img src={match.homeTeam.flagUrl} alt="" className="w-8 h-6 object-cover rounded shadow-sm border border-slate-100" />
            <span className="font-semibold text-primary truncate">{match.homeTeam.name}</span>
          </div>

          <div className="flex flex-col items-center justify-center w-1/3">
            <div className="text-xl font-black text-slate-800 font-mono">
              {match.status === MatchStatus.SCHEDULED ? 'VS' : `${match.homeScore} - ${match.awayScore}`}
            </div>
            <div className="mt-2">
               {prediction ? (
                 <div className="flex items-center gap-1 text-[10px] font-bold text-emerald-600 bg-emerald-50 px-2 py-0.5 rounded-full border border-emerald-100">
                   <Check size={10} />
                   <span>{prediction.homeScore}-{prediction.awayScore}</span>
                 </div>
               ) : (
                 !isFinished && !isLocked && (
                   <div className={`text-[10px] font-bold px-2 py-0.5 rounded-full transition-colors ${
                     isExpanded ? 'bg-accent text-white' : 'text-slate-400 bg-slate-100'
                   }`}>
                     TIPPA
                   </div>
                 )
               )}
               {(isFinished || isLocked) && !prediction && (
                 <div className="text-[10px] font-bold text-slate-300 bg-slate-50 px-2 py-0.5 rounded-full uppercase italic">
                   Ej tippad
                 </div>
               )}
            </div>
          </div>

          <div className="flex items-center gap-3 w-1/3 justify-end text-right">
            <span className="font-semibold text-primary truncate">{match.awayTeam.name}</span>
            <img src={match.awayTeam.flagUrl} alt="" className="w-8 h-6 object-cover rounded shadow-sm border border-slate-100" />
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
              homeTeamName={match.homeTeam.name}
              awayTeamName={match.awayTeam.name}
              homePred={homePred}
              awayPred={awayPred}
              onHomeChange={setHomePred}
              onAwayChange={setAwayPred}
              onSubmit={handleSubmit}
              onCancel={handleCancel}
              isSubmitting={isSubmitting}
              showSuccess={showSuccess}
              hasExistingPrediction={!!prediction}
            />
          </div>
        </div>
      )}
    </div>
  );
};