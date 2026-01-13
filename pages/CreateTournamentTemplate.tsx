
import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth';
import { TournamentTemplateForm } from '../components/tournament/TournamentTemplateForm';
import { TournamentTemplateFormData } from '../hooks/useTournamentTemplateForm';
import { ArrowLeft } from 'lucide-react';
import { createTournamentTemplate } from '../services/api';

/**
 * CreateTournamentTemplate - Administrative page to define a new tournament structure template.
 * Restricted to users with 'admin' username.
 */
export const CreateTournamentTemplate: React.FC = () => {
  const { user, loading } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    // Fail-safe check in case route protection is bypassed
    if (!loading && user?.username !== 'admin') {
      navigate('/tournaments');
    }
  }, [user, loading, navigate]);

  const handleTemplateSubmit = async (data: TournamentTemplateFormData): Promise<boolean> => {
    try {
      await createTournamentTemplate(data);
      setTimeout(() => {
        navigate('/admin/templates');
      }, 1500);
      return true;
    } catch (err) {
      alert("Kunde inte spara mallen.");
      return false;
    }
  };

  const handleCancel = () => {
    navigate('/admin/templates');
  };

  if (loading || user?.username !== 'admin') {
    return null; // Avoid flickering during redirect
  }

  return (
    <div className="animate-fade-in pb-20">
      <div className="max-w-4xl mx-auto mb-8 flex items-center">
        <button 
          onClick={handleCancel}
          className="flex items-center gap-2 text-slate-400 hover:text-primary transition-colors font-bold text-sm"
        >
          <ArrowLeft size={16} />
          Tillbaka
        </button>
      </div>
      
      <TournamentTemplateForm 
        onSubmit={handleTemplateSubmit}
        onCancel={handleCancel}
      />
    </div>
  );
};
