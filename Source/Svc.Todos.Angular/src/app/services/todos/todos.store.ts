import { Injectable } from '@angular/core';
import { ActiveState, EntityState, EntityStore, StoreConfig } from '@datorama/akita';
import { TodoDto } from "../../models/data-contracts";

export interface TodosState extends EntityState<TodoDto>, ActiveState {}

const initialState = {
  active: undefined
};

@Injectable({ providedIn: 'root' })
@StoreConfig({ name: 'todos' })
export class TodosStore extends EntityStore<TodosState> {

  constructor() {
    super(initialState);
  }

}
