import { ChildDetails } from './ChildDetails';
import { Graduate } from './Graduate';
import { YeshivaStudent } from './YeshivaStudent';

export interface SearchResult {
  child?: ChildDetails;
  graduateMatch?: Graduate[];
  yeshivaStudentMatch?: YeshivaStudent[];
}
