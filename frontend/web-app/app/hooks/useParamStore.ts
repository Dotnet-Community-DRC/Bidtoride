import { create } from 'zustand';

type State = {
  pageNumber: number;
  pageSize: number;
  pagecount: number;
  searchTerm: string;
  searchValue: string;
  orderBy: string;
  filterBy: string;
};

type Actions = {
  setParams: (params: Partial<State>) => void;
  reset: () => void;
  setSearchValue: (value: string) => void;
};

const initalState: State = {
  pageNumber: 1,
  pageSize: 12,
  pagecount: 1,
  searchTerm: '',
  searchValue: '',
  orderBy: 'make',
  filterBy: 'live',
};

export const useParamsStore = create<State & Actions>()(set => ({
  ...initalState,

  setParams: (newParams: Partial<State>) => {
    set(state => {
      if (newParams.pageNumber) {
        return { ...state, pageNumber: newParams.pageNumber };
      } else {
        return { ...state, ...newParams, pageNumber: 1 };
      }
    });
  },

  reset: () => set(initalState),
  setSearchValue: (value: string) => {
    set({ searchValue: value });
  },
}));
