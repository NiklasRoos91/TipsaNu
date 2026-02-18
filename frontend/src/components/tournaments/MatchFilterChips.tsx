import React from 'react';
import { Filter } from 'lucide-react';
import { MatchTypeEnum } from '../../types/enums/matchEnums';

interface MatchFilterChipsProps {
  filters: (string | MatchTypeEnum)[];
  selectedFilter: string | MatchTypeEnum | null;
  onSelectFilter: (filter: string | MatchTypeEnum) => void;
  filteredCount: number;
}

export const MatchFilterChips: React.FC<MatchFilterChipsProps> = ({
  filters,
  selectedFilter,
  onSelectFilter,
  filteredCount
}) => {
  return (
    <div className="flex flex-col">
      <div className="flex flex-wrap gap-2 py-2">
        {filters.length > 0 ? (
          filters.map(filter => (
            <button
              key={filter.toString()}
              onClick={() => onSelectFilter(filter)}
              className={`px-3 py-1.5 md:px-4 md:py-2 rounded-full text-[11px] md:text-xs font-bold border transition-all ${
                selectedFilter === filter
                  ? 'bg-accent border-accent text-white shadow-md'
                  : 'bg-white border-slate-200 text-slate-500 hover:border-accent hover:text-accent'
              }`}
            >
              {filter.toString()}
            </button>
          ))
        ) : (
          <p className="text-sm text-slate-400 italic py-2">
            Inga matcher tillgängliga för denna kategori.
          </p>
        )}
      </div>

      {selectedFilter && (
        <div className="flex items-center gap-2 text-xs font-bold text-slate-400 uppercase tracking-wider mb-2">
          <Filter size={14} />
          <span>Visar {filteredCount} matcher i {selectedFilter.toString()}</span>
        </div>
      )}
    </div>
  );
};
