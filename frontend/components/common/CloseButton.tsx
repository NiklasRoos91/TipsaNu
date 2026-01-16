
import React from 'react';
import { XCircle, LucideIcon } from 'lucide-react';

interface CloseButtonProps {
  label?: string;
  onClick: () => void;
  icon?: LucideIcon;
  size?: 'sm' | 'md';
  fullWidth?: boolean;
  className?: string;
}

export const CloseButton: React.FC<CloseButtonProps> = ({
  label = 'Stäng',
  onClick,
  icon: Icon,
  size = 'sm',
  fullWidth = false,
  className = ''
}) => {
  const isSmall = size === 'sm';
  
  const baseStyles = `
    inline-flex items-center justify-center gap-2 rounded-xl font-bold transition-all 
    bg-slate-200 text-slate-700 border-2 border-slate-200 hover:bg-slate-300 active:scale-95 
    uppercase tracking-wider shadow-none leading-none
  `;
  
  const sizeStyles = isSmall 
    ? "px-4 py-1.5 text-[11px]" 
    : "px-6 py-2.5 text-xs tracking-widest";

  return (
    <button 
      type="button"
      onClick={onClick}
      className={`${baseStyles} ${sizeStyles} ${fullWidth ? 'w-full' : 'w-auto'} ${className}`}
    >
      {Icon && <Icon size={isSmall ? 14 : 16} />}
      <span className={isSmall ? "pt-0.5" : ""}>{label}</span>
    </button>
  );
};
