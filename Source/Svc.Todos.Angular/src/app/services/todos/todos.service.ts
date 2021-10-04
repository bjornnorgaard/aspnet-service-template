import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ID } from '@datorama/akita';
import { tap } from 'rxjs/operators';
import { Todo } from './todo.model';
import { TodosStore } from './todos.store';

@Injectable({ providedIn: 'root' })
export class TodosService {

  constructor(private todosStore: TodosStore, private http: HttpClient) {
  }


  get() {
    return this.http.get<Todo[]>('https://api.com').pipe(tap(entities => {
      this.todosStore.set(entities);
    }));
  }

  add(todo: Todo) {
    this.todosStore.add(todo);
  }

  update(id, todo: Partial<Todo>) {
    this.todosStore.update(id, todo);
  }

  remove(id: ID) {
    this.todosStore.remove(id);
  }

}
