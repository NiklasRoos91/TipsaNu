
import React from 'react';
import { LucideIcon } from 'lucide-react';

interface UrlInputProps {
  label: string;
  value: string;
  onChange: (val: string) => void;
  icon: LucideIcon;
  placeholder?: string;
  required?: boolean;
}

export const UrlInput: React.FC<UrlInputProps> = ({
  label,
  value,
  onChange,
  icon: Icon,
  placeholder,
  required = false
}) => (
  <div>
    <label className="block text-xs font-bold text-slate-400 uppercase tracking-widest mb-1.5 ml-1">
      {label} {required && '*'}
    </label>
    <div className="relative">
      <Icon size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
      <input 
        type="url"
        value={value}
        onChange={e => onChange(e.target.value)}
        className="w-full pl-10 pr-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none transition-all font-semibold text-slate-900 shadow-inner"
        placeholder={placeholder}
        required={required}
      />
    </div>
  </div>
);
