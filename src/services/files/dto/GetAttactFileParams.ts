import { PagedFilterAndSortedRequest } from "../../dto/pagedFilterAndSortedRequest";

export interface GetAttactFileParams extends PagedFilterAndSortedRequest{
    year: string;
    id: string;
}