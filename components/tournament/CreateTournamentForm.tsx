
import React from 'react';
import { Trophy, Calendar, Image as ImageIcon } from 'lucide-react';
import { useTournamentForm, TournamentFormData } from '../../hooks/useTournamentForm';
import { FormButtons } from '../common/FormButtons';
import { TextInput } from '../common/inputs/TextInput';
import { DateInput } from '../common/inputs/DateInput';
import { UrlInput } from '../common/inputs/UrlInput';

/**
 * CreateTournamentForm - A modular component for creating a new tournament.
 * Uses specialized inputs and handles internal state through useTournamentForm hook.
 */

interface CreateTournamentFormProps {
  onSubmit: (data: TournamentFormData) => Promise<boolean>;
  onCancel: () => void;
  // Optional: Allows parent to react to state changes (e.g., for live preview)
  onFormChange?: (data: TournamentFormData) => void;
}

export const CreateTournamentForm: React.FC<CreateTournamentFormProps> = ({ 
  onSubmit, 
  onCancel,
  onFormChange 
}) => {
  const {
    formData,
    isSubmitting,
    showSuccess,
    handleChange,
    handleSubmit,
    handleCancel
  } = useTournamentForm({ 
    onSubmit, 
    onCancel 
  });

  // Notify parent of changes if callback exists
  React.useEffect(() => {
    if (onFormChange) {
      onFormChange(formData);
    }
  }, [formData, onFormChange]);

  return (
    <div className="bg-white border border-slate-200 rounded-2xl p-6 shadow-sm animate-fade-in">
      <form onSubmit={handleSubmit} className="grid md:grid-cols-2 gap-6">
        <div className="md:col-span-2">
          <TextInput 
            label="Turneringens Namn"
            value={formData.name}
            onChange={(val) => handleChange('name', val)}
            icon={Trophy}
            placeholder="T.ex. Fotbolls-VM 2026"
            required
          />
        </div>

        <DateInput 
          label="Startdatum"
          value={formData.startDate}
          onChange={(val) => handleChange('startDate', val)}
          icon={Calendar}
          required
        />

        <DateInput 
          label="Slutdatum"
          value={formData.endDate}
          onChange={(val) => handleChange('endDate', val)}
          icon={Calendar}
          required
        />

        <div className="md:col-span-2">
          <UrlInput 
            label="Banner-URL (Bild)"
            value={formData.bannerUrl}
            onChange={(val) => handleChange('bannerUrl', val)}
            icon={ImageIcon}
            placeholder="https://images.unsplash.com/..."
          />
        </div>

        <div className="md:col-span-2 pt-4 border-t border-slate-100">
          <FormButtons 
            isSubmitting={isSubmitting}
            showSuccess={showSuccess}
            onCancel={handleCancel}
            isCreate={true}
            saveLabel="Turnering"
            successLabel="Skapad & Klar!"
            layout="row"
            variant="accent"
          />
        </div>
      </form>
    </div>
  );
};
