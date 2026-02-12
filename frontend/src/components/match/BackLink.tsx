import React from 'react';
import { Link } from 'react-router-dom';
import { ArrowLeft } from 'lucide-react';

interface BackLinkProps {
  tournamentId: number;
}

export const BackLink: React.FC<BackLinkProps> = ({ tournamentId }) => (
  <Link 
    to={`/tournaments/${tournamentId}`} 
    className="inline-flex items-center text-slate-500 hover:text-primary mb-4 transition-colors"
  >
    <ArrowLeft size={18} className="mr-1" /> Tillbaka till turnering
  </Link>
);