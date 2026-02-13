
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { TournamentTemplate } from '../types/types';
import { Layout, FileText, Users, ArrowRight } from 'lucide-react';
import { ActionButton } from '../components/commons/ActionButton';

export const TournamentTemplateList: React.FC = () => {
  const [templates, setTemplates] = useState<TournamentTemplate[]>([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

const getTournamentTemplates = async (): Promise<any[]> => {
  return [
    {
      id: 1,
      name: 'VM 2026',
      description: 'Mockad beskrivning',
      totalGroups: 8,
      advancingPerGroup: 2,
      allowsBestThird: true,
      pointRules: []
    },
    {
      id: 2,
      name: 'EM 2024',
      description: 'Mockad beskrivning',
      totalGroups: 6,
      advancingPerGroup: 2,
      allowsBestThird: false,
      pointRules: []
    }
  ];
};


  useEffect(() => {
    getTournamentTemplates().then(data => {
      setTemplates(data);
      setLoading(false);
    });
  }, []);

  return (
    <div className="max-w-4xl mx-auto pb-20">
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 mb-8">
        <div>
          <h2 className="text-3xl font-bold text-primary flex items-center gap-3">
            <Layout className="text-purple-600" />
            Turneringsmallar
          </h2>
          <p className="text-slate-500 mt-1 font-medium">Hantera strukturer och poängregler för dina turneringar.</p>
        </div>
        
        <div className="w-auto ml-auto">
          <ActionButton 
            label="Skapa ny" 
            onClick={() => navigate('/admin/templates/new')} 
          />
        </div>
      </div>

      {loading ? (
        <div className="text-center p-20 animate-pulse text-slate-400 font-medium">Laddar mallar...</div>
      ) : (
        <div className="grid gap-6 animate-fade-in">
          {templates.length > 0 ? (
            templates.map(tpl => (
              <div 
                key={tpl.id}
                className="bg-white p-6 rounded-2xl border border-slate-200 shadow-sm hover:border-purple-300 transition-all group"
              >
                <div className="flex justify-between items-start mb-4">
                  <div className="flex items-center gap-3">
                    <div className="p-2 bg-purple-50 text-purple-600 rounded-lg">
                      <FileText size={20} />
                    </div>
                    <div>
                      <h3 className="font-bold text-lg text-primary group-hover:text-purple-600 transition-colors">
                        {tpl.name}
                      </h3>
                      <p className="text-sm text-slate-500 mt-0.5">{tpl.description}</p>
                    </div>
                  </div>
                  <div className="flex items-center gap-2 text-purple-600 opacity-0 group-hover:opacity-100 transition-all font-bold text-xs uppercase tracking-widest">
                    Hantera <ArrowRight size={14} />
                  </div>
                </div>

                <div className="grid grid-cols-2 sm:grid-cols-4 gap-4 mt-6 pt-6 border-t border-slate-50">
                  <div>
                    <span className="block text-[10px] font-black text-slate-400 uppercase tracking-widest mb-1">Grupper</span>
                    <div className="flex items-center gap-1.5 font-bold text-slate-700">
                      <Users size={14} className="text-slate-300" />
                      {tpl.totalGroups} st
                    </div>
                  </div>
                  <div>
                    <span className="block text-[10px] font-black text-slate-400 uppercase tracking-widest mb-1">Vidare/grupp</span>
                    <div className="font-bold text-slate-700">{tpl.advancingPerGroup} lag</div>
                  </div>
                  <div>
                    <span className="block text-[10px] font-black text-slate-400 uppercase tracking-widest mb-1">Bästa treor</span>
                    <div className={`text-xs font-bold uppercase tracking-tight ${tpl.allowsBestThird ? 'text-emerald-500' : 'text-slate-300'}`}>
                      {tpl.allowsBestThird ? 'JA' : 'NEJ'}
                    </div>
                  </div>
                  <div>
                    <span className="block text-[10px] font-black text-slate-400 uppercase tracking-widest mb-1">Poängregler</span>
                    <div className="font-bold text-slate-700">{tpl.pointRules.length} st</div>
                  </div>
                </div>
              </div>
            ))
          ) : (
            <div className="text-center p-20 bg-white rounded-3xl border-2 border-dashed border-slate-200">
              <Layout size={48} className="mx-auto text-slate-200 mb-4" />
              <p className="text-slate-400">Inga mallar skapade ännu.</p>
              <button 
                onClick={() => navigate('/admin/templates/new')}
                className="mt-4 text-purple-600 font-bold hover:underline"
              >
                Skapa din första mall nu
              </button>
            </div>
          )}
        </div>
      )}
    </div>
  );
};
