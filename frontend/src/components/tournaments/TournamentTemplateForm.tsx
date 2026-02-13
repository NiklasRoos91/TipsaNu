
import React from 'react';
import { Layout, FileText, Info } from 'lucide-react';
import { useTournamentTemplateForm, TournamentTemplateFormData } from '../../hooks/useTournamentTemplateForm';
import { TextInput } from '../commons/inputs/TextInput';
import { FormButtons } from '../commons/FormButtons';
import { GroupSettingsInput } from './GroupSettingsInput';
import { PointRulesInput } from './PointRulesInput';
import { TiebreakersInput } from './TiebreakersInput';

interface TournamentTemplateFormProps {
  onSubmit: (data: TournamentTemplateFormData) => Promise<boolean>;
  onCancel: () => void;
}

export const TournamentTemplateForm: React.FC<TournamentTemplateFormProps> = ({ onSubmit, onCancel }) => {
  const {
    formData,
    isSubmitting,
    showSuccess,
    handleChange,
    handleSubmit,
    handleCancel
  } = useTournamentTemplateForm({ onSubmit, onCancel });

  return (
    <div className="bg-white border border-slate-200 rounded-3xl p-6 md:p-8 shadow-sm animate-fade-in max-w-4xl mx-auto">
      <div className="flex items-center gap-3 mb-8 pb-4 border-b border-slate-100">
        <div className="p-2.5 bg-primary/5 rounded-xl text-primary">
          <Layout size={24} />
        </div>
        <div>
          <h2 className="text-xl font-bold text-primary">Skapa Turneringsmall</h2>
          <p className="text-sm text-slate-400 font-medium">Definiera regler för gruppspel, poäng och tiebreakers.</p>
        </div>
      </div>

      <form onSubmit={handleSubmit} className="space-y-8">
        {/* Basic Info */}
        <div className="grid md:grid-cols-2 gap-6">
          <div className="md:col-span-2">
            <TextInput 
              label="Mallens Namn"
              value={formData.name}
              onChange={val => handleChange('name', val)}
              icon={FileText}
              placeholder="T.ex. Standard VM-Format"
              required
            />
          </div>
          <div className="md:col-span-2">
            <label className="block text-xs font-bold text-slate-400 uppercase tracking-widest mb-1.5 ml-1">
              Beskrivning *
            </label>
            <div className="relative">
              <Info size={18} className="absolute left-3 top-3.5 text-slate-400" />
              <textarea 
                value={formData.description}
                onChange={e => handleChange('description', e.target.value)}
                className="w-full pl-10 pr-4 py-3 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none transition-all font-semibold text-slate-900 shadow-inner h-24 resize-none"
                placeholder="Beskriv mallen..."
                required
              />
            </div>
          </div>
        </div>

        <div className="grid md:grid-cols-2 gap-10 pt-4 border-t border-slate-50">
          {/* Column 1: Settings */}
          <div className="space-y-10">
            <GroupSettingsInput 
              totalGroups={formData.totalGroups}
              advancingPerGroup={formData.advancingPerGroup}
              allowsBestThird={formData.allowsBestThird}
              onChange={handleChange}
            />
            
            <TiebreakersInput 
              tiebreakers={formData.tiebreakers}
              onChange={val => handleChange('tiebreakers', val)}
            />
          </div>

          {/* Column 2: Point Rules */}
          <div className="space-y-10">
            <PointRulesInput 
              rules={formData.pointRules}
              onChange={val => handleChange('pointRules', val)}
            />
            
            <div className="bg-blue-50/50 p-6 rounded-2xl border border-blue-100 flex gap-4">
              <Info className="text-blue-500 shrink-0" size={20} />
              <p className="text-xs text-blue-700 leading-relaxed font-medium">
                Dessa inställningar kommer att ligga som grund för varje ny turnering du skapar med denna mall. Du kan dock justera specifika värden per turnering senare.
              </p>
            </div>
          </div>
        </div>

        <div className="pt-8 border-t border-slate-100">
          <FormButtons 
            isSubmitting={isSubmitting}
            showSuccess={showSuccess}
            onCancel={handleCancel}
            isCreate={true}
            saveLabel="Mall"
            successLabel="Mall Sparad!"
            layout="row"
          />
        </div>
      </form>
    </div>
  );
};
