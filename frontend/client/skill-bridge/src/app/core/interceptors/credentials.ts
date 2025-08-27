import { HttpInterceptorFn } from '@angular/common/http';

export const credentialsInterceptor: HttpInterceptorFn = (request, next) => {
  const credentialsReq = request.clone({
    withCredentials: true,
  });
  return next(credentialsReq);
};
