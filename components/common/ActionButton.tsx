
import React from 'react';
import { PlusCircle, LucideIcon } from 'lucide-react';
import { CloseButton } from './CloseButton';

interface ActionButtonProps {
  label: string;
  onClick: () => void;
  icon?: LucideIcon;
  variant?: 'primary' | 'secondary' | 'danger';
  fullWidth?: boolean;
  isActive?: boolean;
}

export const ActionButton: React.FC<ActionButtonProps> = ({
  label,
  onClick,
  icon: Icon = PlusCircle,
  variant = 'primary',
  fullWidth = false,
  isActive = false,
}) => {
  if (isActive) {
    return <CloseButton onClick={onClick} fullWidth={fullWidth} />;
  }

  const baseStyles = "inline-flex items-center justify-center gap-2 px-4 py-1.5 rounded-xl font-bold transition-all shadow-sm active:scale-95 text-[11px] uppercase tracking-wider border-2 leading-none";
  
  const variantStyles = {
    primary: "bg-accent border-accent hover:bg-emerald-600 text-white shadow-accent/10",
    secondary: "text-accent border-accent hover:bg-emerald-50 shadow-none",
    danger: "bg-red-500 border-red-500 hover:bg-red-600 text-white shadow-red-200"
  };

  return (
    <button 
      onClick={onClick}
      className={`${baseStyles} ${variantStyles[variant]} ${fullWidth ? 'w-full' : 'w-auto'}`}
    >
      <Icon size={14} />
      <span className="pt-0.5">{label}</span>
    </button>
  );
};
