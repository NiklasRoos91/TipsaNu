
import React from 'react';
import { Shield } from 'lucide-react';
import { ExtraBet, ExtraBetPrediction } from '../../types/types';
import { useExtraBetForm } from '../../hooks/useExtraBetForm';
import { ExtraBetCard } from './ExtraBetCard';
import { ActionButton } from '../common/ActionButton';
import { FormButtons } from '../common/FormButtons';

interface TournamentExtraBetsProps {
  tournamentId: string;
  extraBets: ExtraBet[];
  extraBetPredictions: ExtraBetPrediction[];
  onBetsUpdate: (newBet: ExtraBet) => void;
  onPredictionUpdate: (prediction: ExtraBetPrediction) => void;
  isAdmin: boolean;
}

export const TournamentExtraBets: React.FC<TournamentExtraBetsProps> = ({ 
  tournamentId, 
  extraBets, 
  extraBetPredictions,
  onBetsUpdate, 
  onPredictionUpdate,
  isAdmin 
}) => {
  const extraBetForm = useExtraBetForm(tournamentId, onBetsUpdate);
  const isExpired = (deadline: string) => new Date(deadline) < new Date();

  return (
    <div className="space-y-6">
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
        <h2 className="text-2xl font-bold text-primary">Extratips</h2>
        {isAdmin && (
          <div className="w-auto ml-auto">
            <ActionButton 
              label="Skapa ny" 
              onClick={() => extraBetForm.setShowForm(!extraBetForm.showForm)} 
              isActive={extraBetForm.showForm}
            />
          </div>
        )}
      </div>

      {isAdmin && extraBetForm.showForm && (
        <div className="bg-white border border-slate-200 rounded-2xl p-6 shadow-sm animate-fade-in">
          <div className="flex items-center justify-between mb-4 border-b border-slate-100 pb-3">
            <div className="flex items-center gap-2 text-primary font-bold">
              <Shield size={20} className="text-purple-600" />
              <h3>Admin: Nytt Extratips</h3>
            </div>
          </div>
          <form onSubmit={extraBetForm.handleSubmit} className="grid md:grid-cols-2 gap-4">
            <div className="md:col-span-2">
              <label className="block text-xs font-bold text-slate-400 uppercase mb-1 tracking-widest">Fråga *</label>
              <input 
                value={extraBetForm.formData.question}
                onChange={e => extraBetForm.setFormData({...extraBetForm.formData, question: e.target.value})}
                className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none text-slate-900 font-medium shadow-inner"
                placeholder="Vem vinner skytteligan?"
                required
              />
            </div>
            <div className="md:col-span-2">
              <label className="block text-xs font-bold text-slate-400 uppercase mb-1 tracking-widest">Beskrivning</label>
              <input 
                value={extraBetForm.formData.description}
                onChange={e => extraBetForm.setFormData({...extraBetForm.formData, description: e.target.value})}
                className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none text-slate-900 font-medium shadow-inner"
              />
            </div>
            <div>
              <label className="block text-xs font-bold text-slate-400 uppercase mb-1 tracking-widest">Poäng *</label>
              <input type="number" value={extraBetForm.formData.points} onChange={e => extraBetForm.setFormData({...extraBetForm.formData, points: parseInt(e.target.value)})} className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none text-slate-900 font-medium shadow-inner" required />
            </div>
            <div>
              <label className="block text-xs font-bold text-slate-400 uppercase mb-1 tracking-widest">Deadline *</label>
              <input type="datetime-local" value={extraBetForm.formData.deadline} onChange={e => extraBetForm.setFormData({...extraBetForm.formData, deadline: e.target.value})} className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none text-slate-900 font-medium shadow-inner" required />
            </div>
            <div className="md:col-span-2 bg-slate-50 p-4 rounded-xl border border-slate-200 shadow-inner">
              <label className="block text-xs font-bold text-slate-400 uppercase mb-2 tracking-widest">Alternativ (kommaseparerade)</label>
              <textarea value={extraBetForm.formData.optionsString} onChange={e => extraBetForm.setFormData({...extraBetForm.formData, optionsString: e.target.value})} className="w-full px-4 py-2.5 bg-white border border-slate-200 rounded-xl h-20 outline-none text-slate-900 font-medium focus:ring-4 focus:ring-accent/10 focus:border-accent shadow-sm" placeholder="Mbappé, Kane, Isak..." />
              <div className="flex items-center gap-2 mt-3">
                <input type="checkbox" id="allowCustomTourney" checked={extraBetForm.formData.allowCustom} onChange={e => extraBetForm.setFormData({...extraBetForm.formData, allowCustom: e.target.checked})} className="w-4 h-4 rounded border-slate-300 text-accent focus:ring-accent" />
                <label htmlFor="allowCustomTourney" className="text-xs font-black text-slate-500 uppercase tracking-widest">Tillåt fritext / egna värden</label>
              </div>
            </div>
            <div className="md:col-span-2 mt-2">
              <FormButtons 
                isSubmitting={extraBetForm.isCreating}
                showSuccess={false}
                onCancel={() => extraBetForm.setShowForm(false)}
                isCreate={true}
                saveLabel="Tips"
                layout="row"
              />
            </div>
          </form>
        </div>
      )}

      <div className="grid gap-4">
        {extraBets.map(bet => (
          <ExtraBetCard 
            key={bet.id} 
            bet={bet} 
            initialPrediction={extraBetPredictions.find(p => p.extraBetId === bet.id)}
            isExpired={isExpired(bet.deadline)} 
            onSavePrediction={onPredictionUpdate}
          />
        ))}
        {extraBets.length === 0 && (
          <div className="text-center p-12 bg-slate-50 rounded-xl border-2 border-dashed border-slate-200 text-slate-400">
            Inga extratips skapade för denna turnering ännu.
          </div>
        )}
      </div>
    </div>
  );
};
