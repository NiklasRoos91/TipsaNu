import React, { useState, useEffect } from 'react';
import { AlertCircle, ChevronDown, ChevronUp, Check, Edit2 } from 'lucide-react';
import { FormButtons } from '../commons/FormButtons';
import type { ExtraBetOptionForUser } from '../../types/extrabetTypes';
import { ErrorMessage } from '../commons/ErrorMessage';
import { ExtraBetResultBadge } from './ExtraBetResultBadge';
import { useGetExtraBetOptionCorrectValues } from '../../hooks/extraBets/useGetExtraBetOptionCorrectValues';
import { useSubmitExtraBet } from "../../hooks/extraBets/useSubmitExtraBet";
import { ExtraBetCorrectValuesForm } from './ExtraBetCorrectValuesForm';
import { useAuth } from '../../hooks/useAuth';
import { useGetMyExtraBetByOptionId } from '../../hooks/extraBets/useGetMyExtraBetByOptionId';

export type ExtraBetPrediction = {
  betId: string;
  selectedOption: string;
};

interface ExtraBetCardProps {
  bet: ExtraBetOptionForUser;
  initialPrediction?: ExtraBetPrediction;
  isExpired: boolean;
  onSavePrediction: (prediction: ExtraBetPrediction) => void;
}

export const ExtraBetCard: React.FC<ExtraBetCardProps> = ({ 
  bet, 
  initialPrediction, 
  isExpired,
  onSavePrediction,
}) => {
  const [isExpanded, setIsExpanded] = useState(false);
  const [selectedValue, setSelectedValue] = useState<string>(initialPrediction?.selectedOption || '');
  const [customValue, setCustomValue] = useState<string>('');
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [showSuccess, setShowSuccess] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);    
  const [currentPrediction, setCurrentPrediction] = useState<ExtraBetPrediction | undefined>(undefined);
  const { values: correctValues, loading: loadingCorrectValues, error: correctValuesError } = useGetExtraBetOptionCorrectValues(bet.optionId);
  const { submit, loading: submitting, error: submitError } = useSubmitExtraBet(bet.optionId);
  const { isAdmin } = useAuth();
  const { myBet, loading: loadingMyBet, error: myBetError, refetch: refetchMyBet } = useGetMyExtraBetByOptionId(bet.optionId);

  // Initialize custom value if the prediction doesn't match predefined options
  useEffect(() => {
    const prediction = initialPrediction ?? (myBet ? { betId: myBet.extraBetId.toString(), selectedOption: myBet.value ?? '' } : undefined);
    if (prediction) {
      setSelectedValue(prediction.selectedOption);
      const isPredefined = bet.choices?.some(choice => choice.value === prediction.selectedOption);
      if (!isPredefined && bet.allowCustomChoice) {
        setCustomValue(prediction.selectedOption);
      }
      setCurrentPrediction(prediction);
    }
  }, [initialPrediction, bet.choices, myBet]);

  const handleSelectOption = (val: string) => {
    if (isExpired) return;
    setSelectedValue(val);
    if (val !== 'custom') setCustomValue('');
  };

  const handleCancel = () => {
    const prediction = currentPrediction ?? (myBet ? { betId: myBet.extraBetId.toString(), selectedOption: myBet.value ?? '' } : undefined);
    setSelectedValue(prediction?.selectedOption || '');
    const isPredefined = bet.choices?.some(choice => choice.value === prediction?.selectedOption || '');
    if (!isPredefined && bet.allowCustomChoice) {
      setCustomValue(prediction?.selectedOption || '');
    } else {
      setCustomValue('');
    }
    setIsExpanded(false);
  };

  const handleSubmit = async (e: React.FormEvent) => {
  e.preventDefault();
  if (isExpired || submitting) return;

  const finalValue = selectedValue === 'custom' ? customValue : selectedValue;
  if (!finalValue.trim()) {
    setErrorMessage("Vänligen välj eller skriv ett alternativ.");
    return;
  }

  setErrorMessage(null);

  try {
    const result = await submit({ value: finalValue }); 
    setCurrentPrediction({ betId: bet.optionId.toString(), selectedOption: result.value ?? finalValue });
    setShowSuccess(true);
    setTimeout(() => {
      setShowSuccess(false);
      setIsExpanded(false);
      refetchMyBet();
    }, 2000);
  } catch {
    setErrorMessage("Kunde inte spara ditt tips.");
  }
};

  const hasPrediction = !!currentPrediction;
  const isCustomActive = selectedValue === 'custom' || (selectedValue !== '' && !bet.choices?.some(choice => choice.value === selectedValue));

  if (loadingMyBet) return <div>Laddar ditt tips...</div>;
  if (myBetError) return <ErrorMessage message={myBetError} />;

  return (
    <div 
      className={`bg-white rounded-xl border transition-all overflow-hidden ${
        isExpanded 
          ? 'border-accent ring-1 ring-accent/20 shadow-md' 
          : 'border-slate-200 shadow-sm hover:border-accent hover:shadow-md'
      }`}
    >
      {/* Header View */}
      <div 
        onClick={() => !isExpired && setIsExpanded(!isExpanded)}
        className={`p-6 cursor-pointer select-none transition-colors ${isExpired ? 'cursor-default' : 'hover:bg-slate-50/50'}`}
      >
        <div className="flex justify-between items-start mb-4">
          <div className="flex-1">
            <h3 className="text-lg font-bold text-primary pr-8">{bet.name}</h3>
            {bet.description && <p className="text-sm text-slate-500 mt-1">{bet.description}</p>}
          </div>
          <div className="flex flex-col items-end gap-2">
            <div className="bg-yellow-50 text-yellow-700 px-3 py-1 rounded-lg text-xs font-bold border border-yellow-100 whitespace-nowrap">
              {bet.points}p
            </div>
            {!isExpired && (isExpanded ? <ChevronUp size={20} className="text-accent" /> : <ChevronDown size={20} className="text-slate-300" />)}
          </div>
        </div>

        <div className="flex items-center justify-between mt-2">
          <div className="flex items-center gap-2 text-xs font-bold uppercase tracking-tight">
            <span className={`flex items-center gap-1 ${isExpired ? 'text-red-500' : 'text-slate-400'}`}>
              <AlertCircle size={14} /> {isExpired ? 'Stängd' : `Deadline: ${new Date(bet.expiresAt ?? '').toLocaleString('sv-SE', { day: 'numeric', month: 'short', hour: '2-digit', minute: '2-digit' })}`} {/* CHANGED */}
            </span>
            {isExpired && <span className="bg-red-100 text-red-600 px-2 py-0.5 rounded ml-2">STÄNGD</span>}
          </div>

          <ExtraBetResultBadge 
            prediction={currentPrediction?.selectedOption || null}
            correctAnswer={correctValues?.map(c => c.value) || []}
            isExpanded={isExpanded}
          />
        </div>
      </div>

      {/* Expanded Interactive View */}
      {isExpanded && !isExpired && (
        <div className="border-t border-slate-100 bg-slate-50/50 p-6 animate-fade-in">
          <form onSubmit={handleSubmit} className="max-w-md mx-auto space-y-6">
            <div className="space-y-3">
              <p className="text-xs font-bold text-slate-500 uppercase tracking-widest text-center mb-4">Välj ditt alternativ</p>
              
              <div className="grid grid-cols-1 sm:grid-cols-2 gap-2">
                {bet.choices?.map((choice) => (
                  <button
                    key={choice.choiceId}
                    type="button"
                    onClick={() => handleSelectOption(choice.value)}
                    className={`px-4 py-3 rounded-xl border text-sm font-bold transition-all text-left flex items-center justify-between ${
                      selectedValue === choice.value
                        ? 'bg-accent border-accent text-white shadow-md transform scale-[1.02]' 
                        : 'bg-white border-slate-200 text-slate-600 hover:border-accent hover:text-accent'
                    }`}
                  >
                    {choice.value}
                    {selectedValue === choice.value && <Check size={16} />}
                  </button>
                ))}
                
                {bet.allowCustomChoice && (
                  <button
                    type="button"
                    onClick={() => handleSelectOption('custom')}
                    className={`px-4 py-3 rounded-xl border text-sm font-bold transition-all text-left flex items-center justify-between sm:col-span-2 ${
                      selectedValue === 'custom' || isCustomActive
                        ? 'bg-purple-600 border-purple-600 text-white shadow-md' 
                        : 'bg-white border-slate-200 text-slate-600 hover:border-purple-500 hover:text-purple-500'
                    }`}
                  >
                    <div className="flex items-center gap-2">
                      <Edit2 size={16} />
                      <span>Eget svar</span>
                    </div>
                    {(selectedValue === 'custom' || isCustomActive) && <Check size={16} />}
                  </button>
                )}
              </div>

              {(selectedValue === 'custom' || isCustomActive) && bet.allowCustomChoice && (
                <div className="mt-4 animate-fade-in">
                  <input 
                    type="text"
                    value={customValue}
                    onChange={(e) => setCustomValue(e.target.value)}
                    placeholder="Skriv ditt svar här..."
                    className="w-full px-4 py-3 border-2 border-purple-200 rounded-xl focus:border-purple-500 focus:ring-4 focus:ring-purple-500/10 outline-none transition-all font-medium text-slate-900"
                    autoFocus
                  />
                </div>
              )}

              {errorMessage && <ErrorMessage message={errorMessage} />}
            </div>

            <div className="pt-4">
              <FormButtons 
                isSubmitting={isSubmitting}
                showSuccess={showSuccess}
                hasExistingData={hasPrediction}
                onCancel={handleCancel}
                saveLabel="Tips"
                successLabel="Tippat & Klart!"
              />
            </div>
          </form>
          
          {isAdmin && (
            <>
              <h4 className="tetext-xs font-bold text-slate-500 uppercase tracking-widest text-center mb-4 mt-6">
                Sätt korrektvärden
              </h4>
              <ExtraBetCorrectValuesForm 
                optionId={bet.optionId}
                onCancel={handleCancel}
              />
            </>
          )}
        </div>
      )}
    </div>
  );
};
