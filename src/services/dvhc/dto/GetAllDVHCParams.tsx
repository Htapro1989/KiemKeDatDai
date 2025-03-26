import { PagedFilterAndSortedRequest } from "../../dto/pagedFilterAndSortedRequest";

export interface GetAllDVHCParams extends PagedFilterAndSortedRequest {
    year: any;
    maVung?: any;
    maTinh?: any;
    filter?: any;
}