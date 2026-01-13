
import React from 'react';
import { Save, CheckCircle, PlusCircle, XCircle } from 'lucide-react';
import { CloseButton } from './CloseButton';

interface FormButtonsProps {
  isSubmitting: boolean;
  showSuccess: boolean;
  hasExistingData?: boolean;
  onCancel: () => void;
  saveLabel?: string;
  submitLabel?: string;
  successLabel?: string;
  layout?: 'row' | 'col';
  variant?: 'accent' | 'purple';
  isCreate?: boolean;
}

export const FormButtons: React.FC<FormButtonsProps> = ({
  isSubmitting,
  showSuccess,
  hasExistingData = false,
  onCancel,
  saveLabel = '',
  submitLabel,
  successLabel = 'Sparat & Klart!',
  layout = 'col',
  variant = 'accent',
  isCreate = false
}) => {
  const isRow = layout === 'row';
  
  const getButtonStyles = () => {
    if (showSuccess) return 'bg-emerald-500 hover:bg-emerald-600 text-white';
    if (variant === 'purple') return 'bg-purple-600 hover:bg-purple-700 text-white';
    return 'bg-accent hover:bg-emerald-600 text-white';
  };

  const finalSubmitText = submitLabel || (
    isCreate 
      ? 'Skapa'
      : (hasExistingData ? `Uppdatera ${saveLabel}` : `Spara ${saveLabel}`)
  );

  return (
    <div className={`flex ${isRow ? 'flex-row items-center' : 'flex-col'} gap-3 w-full`}>
      <button
        type="submit"
        disabled={isSubmitting}
        className={`
          ${isRow ? 'w-auto px-8' : 'w-full px-6'} 
          font-bold py-2.5 rounded-xl transition-all shadow-md flex items-center justify-center gap-2 text-sm
          ${getButtonStyles()}
          ${isSubmitting ? 'opacity-50 cursor-not-allowed' : 'hover:shadow-lg active:scale-95'}
          uppercase tracking-widest
        `}
      >
        {isSubmitting ? (
          '...'
        ) : showSuccess ? (
          <><CheckCircle size={16} /> {successLabel}</>
        ) : (
          <>
            {isCreate ? <PlusCircle size={16} /> : <Save size={16} />}
            {finalSubmitText}
          </>
        )}
      </button>
      
      {!showSuccess && (
        <CloseButton 
          label="Avbryt" 
          onClick={onCancel} 
          icon={XCircle} 
          size="md" 
          fullWidth={!isRow}
          className={isRow ? "px-6 bg-slate-100 hover:bg-slate-200 text-slate-500 border-transparent shadow-none" : ""} 
        />
      )}
    </div>
  );
};
