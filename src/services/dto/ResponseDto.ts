export interface ResponseDto<T> {
  code: number;
  errorCode: string;
  message: string;
  returnValue: T;
}
