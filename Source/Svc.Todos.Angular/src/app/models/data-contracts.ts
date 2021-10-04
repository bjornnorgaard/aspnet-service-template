/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface CreateTodoCommand {
  title?: string | null;
  description?: string | null;
}

export interface CreateTodoResult {
  /** @format uuid */
  todoId?: string;
}

export interface DeleteTodoCommand {
  /** @format uuid */
  todoId?: string;
}

export interface GetTodoCommand {
  /** @format uuid */
  todoId?: string;
}

export interface GetTodoResult {
  todo?: TodoDto;
}

export interface GetTodosCommand {
  /** @format int32 */
  pageNumber?: number;

  /** @format int32 */
  pageSize?: number;
  sortProperty?: string | null;
  sortOrder?: SortOrder;
}

export interface GetTodosResult {
  todos?: TodoDto[] | null;
}

export enum SortOrder {
  None = "None",
  Desc = "Desc",
  Asc = "Asc",
}

export interface TodoDto {
  /** @format uuid */
  id?: string;
  title?: string | null;
  description?: string | null;
  isCompleted?: boolean;
}

export interface UpdateTodoCommand {
  title?: string | null;
  description?: string | null;
  isCompleted?: boolean;

  /** @format uuid */
  todoId?: string;
}

export interface UpdateTodoResult {
  updatedTodo?: TodoDto;
}
