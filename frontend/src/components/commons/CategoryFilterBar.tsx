import React from "react";
import { ExtraBetOptionStatusEnum, ExtraBetFilterEnum } from "../../types/enums/extraBetEnums"

export const ExtraBetStatusLabels: Record<ExtraBetFilterEnum, string> = {
  [ExtraBetFilterEnum.All]: "Alla",
  [ExtraBetFilterEnum.Open]: "Öppna",
  [ExtraBetFilterEnum.Closed]: "Stängda",
};

interface CategoryFilterBarProps {
  categories: ExtraBetFilterEnum[];
  currentCategory: ExtraBetFilterEnum;
  onCategoryChange: (category: ExtraBetFilterEnum) => void;
}


export const CategoryFilterBar: React.FC<CategoryFilterBarProps> = ({
  categories,
  currentCategory,
  onCategoryChange,
}) => {
  return (
    <div className="inline-flex bg-slate-100 p-1 rounded-lg gap-1">
      {categories.map((cat) => (
        <button
          key={cat}
          onClick={() => onCategoryChange(cat)}
          className={`px-4 py-1.5 text-xs font-bold rounded-md transition-all ${
            currentCategory === cat
              ? "bg-white text-primary shadow-sm"
              : "text-slate-500 hover:text-slate-700"
          }`}
        >
          {ExtraBetStatusLabels[cat]}
        </button>
      ))}
    </div>
  );
};
