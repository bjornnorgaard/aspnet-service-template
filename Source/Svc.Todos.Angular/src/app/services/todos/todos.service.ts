import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { TodosStore } from './todos.store';
import { environment } from "../../../environments/environment";
import {
  CreateTodoCommand,
  CreateTodoResult,
  DeleteTodoCommand,
  GetTodoCommand,
  GetTodoResult,
  GetTodosCommand,
  GetTodosResult,
  TodoDto,
  UpdateTodoCommand,
  UpdateTodoResult
} from "../../models/data-contracts";
import { Observable } from "rxjs";

@Injectable({providedIn: 'root'})
export class TodosService {

  private baseUrl = `${environment.todoApi}/todos`;

  constructor(private todosStore: TodosStore, private http: HttpClient) {
  }

  getTodos(request: GetTodosCommand): Observable<GetTodosResult> {
    const url = `${this.baseUrl}/get-todos`;
    return this.http.post<GetTodosResult>(url, request).pipe(
      tap(res => this.todosStore.set(res.todos as TodoDto[]))
    );
  }

  public getTodo(request: GetTodoCommand): Observable<GetTodoResult> {
    const url = `${this.baseUrl}/get-todo`;
    return this.http.post<GetTodoResult>(url, request).pipe(
      tap(res => this.todosStore.upsert(request.todoId, res.todo as TodoDto))
    );
  }

  public createTodo(request: CreateTodoCommand): Observable<CreateTodoResult> {
    const url = `${this.baseUrl}/create-todo`;
    return this.http.post<CreateTodoResult>(url, request).pipe(
      tap(res => this.todosStore.upsert(res.createdTodo?.id, res.createdTodo as TodoDto))
    );
  }

  public updateTodo(request: UpdateTodoCommand): Observable<UpdateTodoResult> {
    const url = `${this.baseUrl}/update-todo`;
    return this.http.post<UpdateTodoResult>(url, request).pipe(
      tap(res => this.todosStore.upsert(res.updatedTodo?.id, res.updatedTodo as TodoDto))
    );
  }

  public deleteTodo(request: DeleteTodoCommand): Observable<void> {
    const url = `${this.baseUrl}/delete-todo`;
    return this.http.post<void>(url, request).pipe(
      tap(() => this.todosStore.remove(request.todoId))
    );
  }

}
