
import React from 'react';
import { LogIn, XCircle } from 'lucide-react';
import { CloseButton } from './CloseButton';

interface JoinButtonsProps {
  isProcessing: boolean;
  onCancel: () => void;
  label?: string;
  layout?: 'row' | 'col';
}

export const JoinButtons: React.FC<JoinButtonsProps> = ({ 
  isProcessing, 
  onCancel, 
  label = 'Gå med',
  layout = 'col'
}) => {
  const isRow = layout === 'row';

  return (
    <div className={`flex ${isRow ? 'flex-row items-center' : 'flex-col'} gap-3 w-full`}>
      <button 
        type="submit" 
        disabled={isProcessing} 
        className={`
          ${isRow ? 'w-auto px-8' : 'w-full px-6'}
          bg-blue-600 text-white py-2.5 rounded-xl font-bold shadow-md hover:bg-blue-700 transition-all text-sm uppercase tracking-widest flex items-center justify-center gap-2
          ${isProcessing ? 'opacity-50 cursor-not-allowed' : 'hover:shadow-lg active:scale-95'}
        `}
      >
        {isProcessing ? '...' : <><LogIn size={16} /> {label}</>}
      </button>
      
      <CloseButton 
        label="Avbryt" 
        onClick={onCancel} 
        icon={XCircle} 
        size="md" 
        fullWidth={!isRow} 
        className={isRow ? "px-6 bg-slate-100 hover:bg-slate-200 text-slate-500 border-transparent shadow-none" : ""}
      />
    </div>
  );
};
