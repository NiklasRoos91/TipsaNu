import React from "react";
import { ExtraBetFilterEnum } from "../../types/enums/extraBetEnums"

export const ExtraBetStatusLabels: Record<ExtraBetFilterEnum, string> = {
  [ExtraBetFilterEnum.All]: "Alla",
  [ExtraBetFilterEnum.Open]: "Öppna",
  [ExtraBetFilterEnum.Closed]: "Stängda",
  [ExtraBetFilterEnum.NeedsCorrection]: "Att rätta",
};

interface CategoryFilterBarProps {
  categories: ExtraBetFilterEnum[];
  currentCategory: ExtraBetFilterEnum;
  onCategoryChange: (category: ExtraBetFilterEnum) => void;
  badges?: Partial<Record<ExtraBetFilterEnum, number>>;
}

export const CategoryFilterBar: React.FC<CategoryFilterBarProps> = ({
  categories,
  currentCategory,
  onCategoryChange,
  badges,
}) => {
  return (
    <div className="inline-flex bg-slate-100 p-1 rounded-lg gap-1">
      {categories.map((cat) => {
        const badge = badges?.[cat];
        return (
          <button
            key={cat}
            onClick={() => onCategoryChange(cat)}
            className={`relative px-4 py-1.5 text-xs font-bold rounded-md transition-all ${
              currentCategory === cat
                ? "bg-white text-primary shadow-sm"
                : "text-slate-500 hover:text-slate-700"
            }`}
          >
            {ExtraBetStatusLabels[cat]}
            {badge !== undefined && badge > 0 && (
              <span className="absolute -top-1.5 -right-1.5 min-w-[16px] h-4 px-1 bg-red-500 text-white text-[9px] font-black rounded-full flex items-center justify-center leading-none">
                {badge}
              </span>
            )}
          </button>
        );
      })}
    </div>
  );
};
